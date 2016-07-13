namespace MetricsFacade.Collections
{
    public interface IGaugeCollection
    {
        void Register(string name);
        void AddGauge(string name, double value);
        double GetGauge(string name);
    }
}
