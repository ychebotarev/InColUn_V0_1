using System;
using System.Globalization;
using miniMetrics.Metric;
using miniMetrics.Utils;

namespace miniMetrics.Reports.Formatters
{
    class HumanFormatter : BaseFormatter
    {
        public HumanFormatter(Action<string> callback):base(callback)
        {
        }

        public override void StartContext(string contextName)
        {
            this.callback(string.Format(@"Start context: {0}/n", contextName));
        }

        public override void StartReport(string reportName)
        {
            this.callback(string.Format(@"Start Report: {0}. Time:{2}/n", reportName, DateTime.Now));
        }

        public override void StartMetricGroup(string metricGroup)
        {
            this.callback(string.Format(@"Start Group: {0}/n", metricGroup));
        }

        public override void EndMetricGroup(string metricGroup)
        {
            this.callback(string.Format(@"End Group: {0}/n", metricGroup));
        }

        public override void EndReport(string reportName)
        {
            this.callback(string.Format(@"End Report: {0}/n", reportName));
        }

        public override void EndContext(string contextName)
        {
            this.callback(string.Format(@"End Context: {0}/n", contextName));
        }

        public override void ReportGauge(string name, double value, Unit unit)
        {
            this.callback(string.Format(@"{0} : {1}/n"
                , name
                , value.ToString("F2", CultureInfo.InvariantCulture)
                , unit));
        }

        public override void ReportCounter(string name, Counter counter, Unit unit)
        {
            this.callback(string.Format(@"Counter. Name:{0}. Value:{1}/n"
                , name
                , counter.Value.ToString()
                , unit));
        }

        public override void ReportMeter(string name, Meter meter, Unit unit, TimeUnit rateUnit)
        {
            this.callback(string.Format(@"Meter. Name:{0}. Value:{1} {2}/{3}/n"
                , name
                , meter.Value.ToString("F2", CultureInfo.InvariantCulture)
                , unit
                , rateUnit));
        }

        public override void ReportTimer(string name, TimeInterval timer, TimeUnit resolution)
        {
            foreach(var tick in timer.ticks)
            {
                this.callback(string.Format(@"Timer. Name:{0}. Start:{1}. End:{2}. Duration:{3}/n"
                    , name
                    , TimeUnit.Nanoseconds.Convert(resolution, tick.Item1)
                    , TimeUnit.Nanoseconds.Convert(resolution, tick.Item2)
                    , TimeUnit.Nanoseconds.Convert(resolution, tick.Item2 - tick.Item1)));
            }
        }
    }
}
