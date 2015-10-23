﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huoyaoyuan.AdmiralRoom.Officer
{
    public static class StaticCounters
    {
        public static CounterBase CVCounter { get; } = new CounterBase();
        public static CounterBase SSCounter { get; } = new CounterBase();
        public static CounterBase TransportCounter { get; } = new CounterBase();
        public static CounterBase BossCounter { get; } = new CounterBase();
        public static CounterBase BossWinCounter { get; } = new CounterBase();
        public static CounterBase SortieCounter { get; } = new CounterBase();
        public static CounterBase SRankCounter { get; } = new CounterBase();
        public static CounterBase Map2Counter { get; } = new CounterBase();
        public static CounterBase Map3Counter { get; } = new CounterBase();
        public static CounterBase Map4Counter { get; } = new CounterBase();
        public static CounterBase Map1_5Counter { get; } = new CounterBase();
    }
}
