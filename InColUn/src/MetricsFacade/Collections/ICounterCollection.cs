namespace MetricsFacade.Collections
{
    public interface ICounterCollection
    {
        void AddCounter(string name);
        void AddCounter(string name, long value);

        void Increment(string name);
        void Increment(string name, long step);

        void Decrement(string name);
        void Decrement(string name, long step);

        long GetValue(string name);
    }
}
