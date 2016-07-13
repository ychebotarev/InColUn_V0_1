using miniMetrics.Collections;

namespace miniMetrics
{
    class MetricsService
    {
        public static MetricsService gi = new MetricsService("global");

        public CounterCollection Counters = new CounterCollection();
        public MeterCollection Rates = new MeterCollection();
        public SnapshotCollectiion Snapshots = new SnapshotCollectiion();
        public TimeIntervalCollection Intervals = new TimeIntervalCollection();
        public GaugeCollection Gauges = new GaugeCollection();

        public MetricsService(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}