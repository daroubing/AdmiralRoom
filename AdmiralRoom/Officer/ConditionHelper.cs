﻿using System;

namespace Huoyaoyuan.AdmiralRoom.Officer
{
    public class ConditionHelper
    {
        public static ConditionHelper Instance { get; } = new ConditionHelper();
        private ConditionHelper() { }
        private int maxup;
        private DateTime increasefrom, increaseto, lastcheck;
        private bool updating;
        private bool changed = false;
        private readonly TimeSpan maxerror = TimeSpan.FromSeconds(2), period = TimeSpan.FromMinutes(3);
        public void OnCondition(int d)
        {
            if (updating)
                maxup = d > maxup ? d : maxup;
        }
        public void BeginUpdate()
        {
            updating = true;
            maxup = 0;
        }
        public void EndUpdate()
        {
            updating = false;
            var checktime = DateTime.UtcNow;
            if (increasefrom == DateTime.MinValue)//first time
            {
                lastcheck = checktime;
                increasefrom = checktime - period;
                increaseto = checktime;
                changed = true;
                return;
            }
            if (maxup == 0)
            {
                lastcheck = checktime;
                while (increaseto + period + maxerror < checktime)
                {
                    increasefrom += period;
                    increaseto += period;
                    changed = true;
                }
                return;
            }
            maxup = (int)Math.Ceiling(maxup / 3.0) * 3;
            increasefrom += TimeSpan.FromMinutes(maxup);
            increaseto += TimeSpan.FromMinutes(maxup);
            while (increaseto + maxerror < lastcheck)
            {
                increasefrom += TimeSpan.FromMinutes(3);
                increaseto += TimeSpan.FromMinutes(3);
            }
            if (increasefrom < lastcheck) increasefrom = lastcheck;
            if (increaseto > checktime) increaseto = checktime;
            if (increasefrom > increaseto)//swap
            {
                var temp = increasefrom;
                increasefrom = increaseto;
                increaseto = temp;
            }
            lastcheck = checktime;
            changed = true;
        }
        private DateTime _basetime;
        public DateTime BaseTime
        {
            get
            {
                if (changed)
                    _basetime = increaseto - Offset;
                changed = false;
                return _basetime;
            }
        }
        private TimeSpan _offset;
        public TimeSpan Offset
        {
            get
            {
                if (changed)
                    _offset = TimeSpan.FromSeconds((increaseto - increasefrom).TotalSeconds / 2);
                return _offset;
            }
        }
        public TimeSpan Remain(int cond) => (BaseTime.AddMinutes((int)Math.Ceiling((40 - cond) / 3.0) * 3)).Remain();
    }
}
