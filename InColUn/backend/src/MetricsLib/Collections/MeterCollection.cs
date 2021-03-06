﻿using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;

using miniMetrics.Metric;
using MetricsFacade.Collections;
using MetricsFacade.Metric;

namespace miniMetrics.Collections
{
    public enum MeterIntervals
    {
        tenSec,
        oneMin
    };

    public class MeterGroup
    {
        Timer timer;
        public int Interval { get; private set; }
        public ConcurrentDictionary<string, Meter> meters;

        public MeterGroup(int interval)
        {
            this.Interval = interval;
            this.meters = new ConcurrentDictionary<string, Meter>();

            this.timer = new Timer(OnTimedEvent, this, -1, interval);
        }

        public void Start()
        {
            this.timer.Change(0, this.Interval);
        }

        public void Stop()
        {
            this.timer.Change(-1, this.Interval);
        }

        private static void OnTimedEvent(object state)
        {
            var group = state as MeterGroup;
            if (group != null)
            {
                group.Tick();
            }
        }

        public void Tick()
        {
            foreach (var rateMetric in this.meters.Values)
            {
                rateMetric.Tick();
            }
        }
    }

    public class MeterCollection : Utils.IHideObjectMembers, IMeterCollection
    {
        private Dictionary<MeterIntervals, MeterGroup> rates;

        public MeterCollection()
        {
            rates = new Dictionary<MeterIntervals, MeterGroup>();
            rates[MeterIntervals.tenSec] = new MeterGroup(1000 * 10);
            rates[MeterIntervals.oneMin] = new MeterGroup(1000 * 60);
        }

        public void Stop()
        {
            this.Stop(MeterIntervals.oneMin);
            this.Stop(MeterIntervals.tenSec);
        }

        public void Stop(MeterIntervals rateUnit)
        {
            rates[rateUnit].Stop();
        }

        public void Start()
        {
            this.Start(MeterIntervals.oneMin);
            this.Start(MeterIntervals.tenSec);
        }

        public void Start(MeterIntervals rateUnit)
        {
            rates[rateUnit].Start();
        }

        public double GetMeterRate(string name)
        {
            return this[name].Value;
        }

        public IMeter this[string name]
        {
            get
            {
                var group = this.GetGroup(name);
                return group != null ? group.meters[name] : null;
            }
        }

        public MeterGroup this[MeterIntervals unit]
        {
            get
            {
                if (!this.rates.ContainsKey(unit)) return null;
                return this.rates[unit];
            }
        }

        public void AddMeter(string name, MeterIntervals rateUnit)
        {
            var rateGroup = this.rates[rateUnit];
            if (rateGroup.meters.ContainsKey(name)) return;

            rateGroup.meters[name] = Meter.createM1Rate();
        }

        public void AddMeter(string name)
        {
            this.AddMeter(name, MeterIntervals.tenSec);
        }

        private MeterGroup GetGroup(string name)
        {
            if (this.rates[MeterIntervals.tenSec].meters.ContainsKey(name))
                return this.rates[MeterIntervals.tenSec];
            if (this.rates[MeterIntervals.oneMin].meters.ContainsKey(name))
                return this.rates[MeterIntervals.oneMin];
            return null;
        }

        public bool Increment(string name)
        {
            var group = this.GetGroup(name);
            if (group != null) group.meters[name].Increment();
            return group != null;
        }

        public bool Increment(string name, long value)
        {
            var group = this.GetGroup(name);
            if (group != null) group.meters[name].Update(value);
            return group != null;
        }

        public bool Mark(string name)
        {
            return this.Increment(name);
        }
    }
}