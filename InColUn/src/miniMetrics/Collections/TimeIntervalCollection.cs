using miniMetrics.Metric;
using System.Collections.Generic;

namespace miniMetrics.Collections
{
    class TimeIntervalCollection
    {
        public Dictionary<string, TimeInterval> intervals;

        public TimeIntervalCollection()
        {
            this.intervals = new Dictionary<string, TimeInterval>();
        }

        public void Register(string name)
        {
            if (this.intervals.ContainsKey(name)) return;

            this.intervals[name] = new TimeInterval();
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
