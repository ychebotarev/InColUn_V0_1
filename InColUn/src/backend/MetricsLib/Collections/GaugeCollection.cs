using System.Collections.Concurrent;
using MetricsFacade.Collections;

namespace miniMetrics.Collections
{
    public class GaugeCollection : Utils.IHideObjectMembers, IGaugeCollection
    {
        private ConcurrentDictionary<string, double> gauges = new ConcurrentDictionary<string, double>();

        public void AddGauge(string name)
        {
            if (this.gauges.ContainsKey(name)) return;
            this.gauges[name] = 0.0;
        }

        public void AddGauge(string name, double value)
        {
            this.AddGauge(name);
            this.gauges[name] = value;
        }

        public double GetGauge(string name)
        {
            return this[name];
        }

        public int GetGaugessCount() { return this.gauges.Count; }

        public int Count => this.gauges.Count;


        public double this[string name]
        {
            get { this.AddGauge(name); return this.gauges[name]; }
            set { this.AddGauge(name); this.gauges[name] = value; }
        }
    }
}
