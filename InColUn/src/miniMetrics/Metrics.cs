using miniMetrics.Collections;

namespace miniMetrics
{
    class Metrics
    {
        public static Metrics gi = new Metrics("global");


        public CounterCollection Counters = new CounterCollection();
        public MeterCollection Rates = new MeterCollection();
        public SnapshotCollectiion Snapshots = new SnapshotCollectiion();
        public TimeIntervalCollection Intervals = new TimeIntervalCollection();
        public GaugeCollection Gauges = new GaugeCollection();

        public Metrics(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}