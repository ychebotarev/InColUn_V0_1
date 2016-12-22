namespace MetricsFacade.Metric
{
    public interface IMeter
    {
        void Update(long n);

        void Increment();

        void Mark();

        void Tick();
        double GetRate();
        double Value { get; }
    }
}
