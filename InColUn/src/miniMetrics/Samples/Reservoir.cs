namespace miniMetrics.Samples
{
    public interface Reservoir
    {
        void Update(long value, string userValue = null);
        SampleSnapshot GetSnapshot(bool resetReservoir = false);
        void Reset();
    }
}
