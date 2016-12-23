using MetricsFacade.Collections;

namespace MetricsFacade
{
    public interface IMetricsService
    {
        ICounterCollection Counters { get; }
        IMeterCollection Rates { get; }
        ISnapshotCollectiion Snapshots { get; }
        ITimeIntervalCollection Intervals { get; }
        IGaugeCollection Gauges { get; }
        string Name { get; }


    }
}
