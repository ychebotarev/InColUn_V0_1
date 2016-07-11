using miniMetrics.Metric;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Timers;

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
        public ConcurrentDictionary<string, Meter> meters;

        public MeterGroup(int interval)
        {
            this.meters = new ConcurrentDictionary<string, Meter>();

            this.timer = new Timer();

            this.timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            this.timer.Interval = interval;
            this.timer.Enabled = false;
        }

        public void Start()
        {
            this.timer.Enabled = true;
        }

        public void Stop()
        {
            this.timer.Enabled = false;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var group = source as MeterGroup;
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

    public class MeterCollection
    {
        private Dictionary<MeterIntervals, MeterGroup> rates;

        public MeterCollection()
        {
            rates = new Dictionary<MeterIntervals, MeterGroup>();
            rates[MeterIntervals.tenSec] = new MeterGroup(1000 * 10);
            rates[MeterIntervals.oneMin] = new MeterGroup(1000 * 60);
        }

        public void Stop(MeterIntervals rateUnit)
        {
            rates[rateUnit].Stop();
        }

        public void Start(MeterIntervals rateUnit)
        {
            rates[rateUnit].Start();
        }

        public Meter this[string name]
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

        public void AddRate(string name, MeterIntervals rateUnit)
        {
            var rateGroup = this.rates[rateUnit];
            if (rateGroup.meters.ContainsKey(name)) return;

            rateGroup.meters[name] = Meter.createM1Rate();
        }

        public void AddRate(string name)
        {
            this.AddRate(name, MeterIntervals.tenSec);
        }

        public void Register(string name)
        {
            this.AddRate(name);
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

        public bool Mark(string name)
        {
            return this.Increment(name);
        }

        public bool Update(string name, long value)
        {
            var group = this.GetGroup(name);
            if (group != null) group.meters[name].Update(value);
            return group != null;
        }
    }
}