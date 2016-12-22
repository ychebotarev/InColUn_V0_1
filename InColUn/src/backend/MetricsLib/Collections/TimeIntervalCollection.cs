using System.Collections.Generic;
using System.Collections.Concurrent;

using MetricsFacade.Collections;
using MetricsFacade.Metric;

namespace miniMetrics.Collections
{
    public class TimeIntervalCollection : Utils.IHideObjectMembers, ITimeIntervalCollection
    {
        public Dictionary<string, ConcurrentBag<ITimeInterval>> intervals;

        public TimeIntervalCollection()
        {
            this.intervals = new Dictionary<string, ConcurrentBag<ITimeInterval>>();
        }

        public void AddIntervals(string name)
        {
            if (this.intervals.ContainsKey(name)) return;

            this.intervals[name] = new ConcurrentBag<ITimeInterval>();
        }

        public void AddInterval(string name, ITimeInterval interval)
        {
            this.AddIntervals(name);
            this.intervals[name].Add(interval);
        }

        public void Reset(string name)
        {
            this.intervals[name] = new ConcurrentBag<ITimeInterval>();
        }

        public IEnumerable<ITimeInterval> GetIntervals(string name)
        {
            return this.intervals[name];
        }

        public IEnumerable<ITimeInterval> this[string name]
        {
            get
            {
                if (this.intervals.ContainsKey(name)) return this.intervals[name];

                return null;
            }
        }

        public int GetIntervalsCount() { return this.intervals.Count; }

        public int Count => this.intervals.Count;

    }
}
