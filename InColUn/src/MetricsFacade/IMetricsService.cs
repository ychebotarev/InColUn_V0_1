using MetricsFacade.Collections;

namespace MetricsFacade
{
    public class IMetricsService
    {
        public ICounterCollection CountersCollection { get; protected set; }
        public IMeterCollection RatesCollection { get; protected set; }
        public ISnapshotCollectiion SnapshotsCollection { get; protected set; }
        public ITimeIntervalCollection IntervalsCollection { get; protected set; }
        public IGaugeCollection GaugesCollection { get; protected set; }
        public string Name { get; protected set; }
    }
}
