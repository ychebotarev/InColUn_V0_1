using System;
using MetricsFacade.Metric;

namespace miniMetrics.Metric
{
    class Snapshot : Utils.IHideObjectMembers, ISnapshot
    {
        public double Value { get; private set; }
        public DateTime Date { get; private set; }
        public Snapshot(double value)
        {
            this.Value = value;
            this.Date = DateTime.Now;
        }

        public double GetValue() => this.Value;
        public DateTime GetDate() => this.Date;
    }
}
