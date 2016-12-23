using System.Collections.Concurrent;
using miniMetrics.Metric;
using MetricsFacade.Collections;
using MetricsFacade.Metric;

namespace miniMetrics.Collections
{
    public class CounterCollection : Utils.IHideObjectMembers, ICounterCollection
    {
        public ConcurrentDictionary<string, ICounter> counters = new ConcurrentDictionary<string, ICounter>();

        public void AddCounter(string name)
        {
            if (this.counters.ContainsKey(name)) return;
            this.counters[name] = new Counter();
        }

        public void AddCounter(string name, long value)
        {
            if (this.counters.ContainsKey(name)) return;
            this.counters[name] = new Counter(value);
        }

        public int GetCountersCount() { return this.counters.Count; }

        public int Count => this.counters.Count;

        public void Increment(string name) { this.counters[name].Increment(); }
        public void Increment(string name, long step) { this.counters[name].Increment(step); }
        public void Decrement(string name) { this.counters[name].Decrement(); }
        public void Decrement(string name, long step) { this.counters[name].Decrement(step); }

        public long GetValue(string name)
        {
            return this[name].Value;
        }

        public ICounter this[string name]
        {
            get { return this.counters[name]; }
            set { this.counters[name] = value; }
        }
    }
}
