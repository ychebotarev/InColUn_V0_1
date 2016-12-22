using MetricsFacade.Collections;
using MetricsFacade.Metric;
using miniMetrics.Utils;

namespace miniMetrics.Reports
{
    interface IMetricsReporter
    {
        void ReportGauges(IGaugeCollection collection);
        void ReportIntervals(ITimeIntervalCollection collection);
        void ReportSnapshots(ISnapshotCollectiion collection);
        void ReportCounters(ICounterCollection collection);
        void ReportMeters(IMeterCollection collection);

        void ReportGauge(string name, double value, Unit unit);
        void ReportTimeInterval(string name, ITimeInterval value, TimeUnit resolution);
        void ReportSnapshot(string name, ISnapshot value, Unit unit, TimeUnit rateUnit);
        void ReportCounter(string name, ICounter value, Unit unit);
        void ReportMeter(string name, IMeter value, Unit unit, TimeUnit rateUnit);

        void StartReport(string contextName);
        void StartContext(string contextName);
        void StartMetricGroup(string metricName);
        void EndMetricGroup(string metricName);
        void EndContext(string contextName);
        void EndReport(string contextName);
    }
}
