using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniMetrics.Metric
{
    class Snapshot: Utils.IHideObjectMembers
    {
        public double Value { get; private set; }
        public DateTime Date { get; private set; }
        public Snapshot(double value)
        {
            this.Value = value;
            this.Date = DateTime.Now;
        }
    }
}
