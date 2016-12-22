using miniMetrics.Collections;
using MetricsFacade;
using MetricsFacade.Collections;

namespace miniMetrics
{
    public class MetricsService : IMetricsService
    {
        public static MetricsService gi = new MetricsService("global");

        private CounterCollection counterCollection;
        private MeterCollection meterCollection;
        private SnapshotCollectiion snapshotCollectiion;
        private TimeIntervalCollection timeIntervalCollection;
        private GaugeCollection gaugeCollection;

        private string name;

        public MetricsService(string name)
        {
            this.counterCollection = new CounterCollection();
            this.meterCollection = new MeterCollection();
            this.snapshotCollectiion = new SnapshotCollectiion();
            this.timeIntervalCollection = new TimeIntervalCollection();
            this.gaugeCollection = new GaugeCollection();

            this.name = name;
        }

        public ICounterCollection Counters => this.counterCollection;
        public IMeterCollection Rates => this.meterCollection;
        public ISnapshotCollectiion Snapshots => this.snapshotCollectiion;
        public ITimeIntervalCollection Intervals => this.timeIntervalCollection;
        public IGaugeCollection Gauges => this.gaugeCollection;
        public string Name => this.Name;
    }
}
