using MetricsFacade.Metric;

namespace MetricsFacade.Collections
{
    public interface IMeterCollection
    {
        void Start();
        void Stop();

        void AddMeter(string name);
        double GetMeterRate(string name);

        bool Increment(string name);
        bool Increment(string name, long value);

        IMeter this[string name] { get; }
    }
}
