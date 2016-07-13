using miniMetrics.Collections;
using MetricsFacade;

namespace miniMetrics
{
    class MetricsService : IMetricsService
    {
        public static MetricsService gi = new MetricsService("global");

        public CounterCollection Counters;
        public MeterCollection Rates;
        public SnapshotCollectiion Snapshots;
        public TimeIntervalCollection Intervals;
        public GaugeCollection Gauges;

        public MetricsService(string name)
        {
            this.Counters = new CounterCollection();
            this.Rates = new MeterCollection();
            this.Snapshots = new SnapshotCollectiion();
            this.Intervals = new TimeIntervalCollection();
            this.Gauges = new GaugeCollection();

            this.CountersCollection = this.Counters;

            this.Name = name;
        }

        //public string Name { get; private set; }
    }
}