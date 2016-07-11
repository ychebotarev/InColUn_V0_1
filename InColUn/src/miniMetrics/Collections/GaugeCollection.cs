using System.Collections.Concurrent;
using miniMetrics.Metric;

namespace miniMetrics.Collections
{
    class GaugeCollection
    {
        private ConcurrentDictionary<string, double> _gauges = new ConcurrentDictionary<string, double>();

        public void Register(string name)
        {
            if (this._gauges.ContainsKey(name)) return;
            this._gauges[name] = 0.0;
        }

        public void Add(string name, double value)
        {
            this.Register(name);
            this._gauges[name] = value;
        }

        public double this[string name]
        {
            get { this.Register(name); return this._gauges[name]; }
            set { this.Register(name); this._gauges[name] = value; }
        }
    }
}
