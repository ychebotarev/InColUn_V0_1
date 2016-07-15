using System.Collections.Concurrent;
using miniMetrics.Metric;
using MetricsFacade.Collections;

namespace miniMetrics.Collections
{
    public class GaugeCollection : Utils.IHideObjectMembers, IGaugeCollection
    {
        private ConcurrentDictionary<string, double> gauges = new ConcurrentDictionary<string, double>();

        public void Register(string name)
        {
            if (this.gauges.ContainsKey(name)) return;
            this.gauges[name] = 0.0;
        }

        public void AddGauge(string name, double value)
        {
            this.Register(name);
            this.gauges[name] = value;
        }

        public double GetGauge(string name)
        {
            return this[name];
        }

        public double this[string name]
        {
            get { this.Register(name); return this.gauges[name]; }
            set { this.Register(name); this.gauges[name] = value; }
        }
    }
}
