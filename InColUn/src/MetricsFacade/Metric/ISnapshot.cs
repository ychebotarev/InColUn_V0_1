using System;

namespace MetricsFacade.Metric
{
    public interface ISnapshot
    {
        double GetValue();
        DateTime GetDate();
    }
}
