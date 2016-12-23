using miniMetrics.Metric;
using miniMetrics.Utils;
using System;

namespace miniMetrics.Reports.Formatters
{
    abstract class BaseFormatter
    {
        protected Action<string> callback;

        public BaseFormatter(Action<string> callback)
        {
            this.callback = callback;
        }

        public virtual void StartContext(string contextName) { }
        public virtual void StartReport(string reportName) { }
        public virtual void StartMetricGroup(string groupName) { }
        public virtual void EndMetricGroup(string groupName) { }
        public virtual void EndReport(string reportName) { }
        public virtual void EndContext(string contextName) { }

        public abstract void ReportGauge(string name, double value, Unit unit);
        public abstract void ReportCounter(string name, Counter value, Unit unit);
        public abstract void ReportMeter(string name, Meter value, Unit unit, TimeUnit rateUnit);
        public abstract void ReportTimer(string name, TimeInterval value, TimeUnit resolution);
    }
}