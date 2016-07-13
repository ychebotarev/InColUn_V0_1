using System.Collections.Concurrent;
using miniMetrics.Metric;

namespace miniMetrics.Collections
{
    public class CounterCollection : Utils.IHideObjectMembers, ICounterCollection
    {
        public ConcurrentDictionary<string, Counter> _counters = new ConcurrentDictionary<string, Counter>();

        public void Register(string name)
        {
            if (this._counters.ContainsKey(name)) return;
            this._counters[name] = new Counter();
        }

        public void Register(string name, long value)
        {
            if (this._counters.ContainsKey(name)) return;
            this._counters[name] = new Counter();
        }

        public int GetCountersCount() { return this._counters.Count; }

        public int Count => this._counters.Count;

        public void Increment(string name) { this._counters[name].Increment(); }
        public void Increment(string name, long step) { this._counters[name].Increment(step); }
        public void Decrement(string name) { this._counters[name].Decrement(); }
        public void Decrement(string name, long step) { this._counters[name].Decrement(step); }

        public Counter this[string name]
        {
            get { return this._counters[name]; }
            set { this._counters[name] = value; }
        }
    }
}
