namespace miniMetrics.Collections
{
    public interface ICounterCollection
    {
        void Register(string name);
        void Register(string name, long value);

        int GetCountersCount();

        void Increment(string name);
        void Increment(string name, long step);
        void Decrement(string name);
        void Decrement(string name, long step);
    }
}
