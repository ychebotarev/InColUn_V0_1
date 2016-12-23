using miniMetrics.Utils;

using MetricsFacade.Collections;
using MetricsFacade.Metric;

namespace miniMetrics.Reports
{
    class ConsoleReport : IMetricsReporter
    {
        public void ReportGauges(IGaugeCollection collection)
        {
        }
        public void ReportIntervals(ITimeIntervalCollection collection) { }
        public void ReportSnapshots(ISnapshotCollectiion collection) { }
        public void ReportCounters(ICounterCollection collection) { }
        public void ReportMeters(IMeterCollection collection) { }

        public void ReportGauge(string name, double value, Unit unit) { }
        public void ReportTimeInterval(string name, ITimeInterval value, TimeUnit resolution) { }
        public void ReportSnapshot(string name, ISnapshot value, Unit unit, TimeUnit rateUnit) { }
        public void ReportCounter(string name, ICounter value, Unit unit) { }
        public void ReportMeter(string name, IMeter value, Unit unit, TimeUnit rateUnit) { }

        public void StartReport(string contextName) { }
        public void StartContext(string contextName) { }
        public void StartMetricGroup(string metricName) { }
        public void EndMetricGroup(string metricName) { }
        public void EndContext(string contextName) { }
        public void EndReport(string contextName) { }
    }
}
