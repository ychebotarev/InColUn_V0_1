using System.Collections.Generic;
using MetricsFacade.Metric;

namespace MetricsFacade.Collections
{
    public interface ITimeIntervalCollection
    {
        int GetIntervalsCount();
        void AddIntervals(string name);
        void AddInterval(string name, ITimeInterval interval);

        void Reset(string name);

        IEnumerable<ITimeInterval> GetIntervals(string name);
    }
}
