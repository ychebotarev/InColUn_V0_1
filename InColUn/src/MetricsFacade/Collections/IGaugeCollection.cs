namespace MetricsFacade.Collections
{
    public interface IGaugeCollection
    {
        void AddGauge(string name);
        void AddGauge(string name, double value);
        double GetGauge(string name);
        int GetGaugessCount();
    }
}
