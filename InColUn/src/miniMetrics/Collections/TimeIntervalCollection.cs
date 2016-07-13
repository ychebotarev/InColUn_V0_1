using System.Collections.Generic;

using miniMetrics.Metric;
using MetricsFacade.Collections;

namespace miniMetrics.Collections
{
    class TimeIntervalCollection : Utils.IHideObjectMembers, ITimeIntervalCollection
    {
        public Dictionary<string, TimeInterval> intervals;

        public TimeIntervalCollection()
        {
            this.intervals = new Dictionary<string, TimeInterval>();
        }

        public void AddInterval(string name)
        {
            if (this.intervals.ContainsKey(name)) return;

            this.intervals[name] = new TimeInterval();
        }

        public void Start(string name)
        {
            this[name].Start();
        }

        public void Stop(string name)
        {
            this[name].Stop();
        }

        public void Reset(string name)
        {
            this[name].Reset();
        }

        public long GetIntervals(string name)
        {
            return this[name].Duration;
        }

        public long GetIntervalStartTime(string name)
        {
            return this[name].StartTime;
        }

        public long GetIntervalStopTime(string name)
        {
            return this[name].Duration;
        }

        public TimeInterval this[string name]
        {
            get
            {
                if (this.intervals.ContainsKey(name)) return this.intervals[name];

                return null;
            }
        }
    }
}
