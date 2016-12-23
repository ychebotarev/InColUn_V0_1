namespace MetricsFacade.Metric
{
    public interface ITimeInterval
    {
        void Start();
        void Stop();
        void Reset();

        long GetStartTime();
        long GetEndTime();
        long GetDuration();
    }
}
