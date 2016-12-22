namespace MetricsFacade.Metric
{
    public interface ICounter
    {
        void Increment();
        void Increment(long step);
        void Decrement();
        void Decrement(long step);
        void SetValue(long value);
        long GetValue();

        long Value { get; }
    }
}
