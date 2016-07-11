using miniMetrics.Metric;
using miniMetrics.Utils;
using System;

namespace miniMetrics.Reports
{
    abstract class BaseReporter
    {
        protected virtual void ReportRates() { } 
        protected virtual void ReportIntervals() { }
        protected virtual void ReportSnapshots() { }
        protected virtual void ReportCounters() { }

        protected virtual void StartReport(string contextName) { }
        protected virtual void StartContext(string contextName) { }
        protected virtual void StartMetricGroup(string metricName) { }
        protected virtual void EndMetricGroup(string metricName) { }
        protected virtual void EndContext(string contextName) { }
        protected virtual void EndReport(string contextName) { }

        protected abstract void ReportGauge(string name, double value, Unit unit);
        protected abstract void ReportCounter(string name, Counter value, Unit unit);
        protected abstract void ReportMeter(string name, Meter value, Unit unit, TimeUnit rateUnit);
        protected abstract void ReportTimer(string name, TimeInterval value, TimeUnit resolution);
    }
}
