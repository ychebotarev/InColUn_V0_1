using miniMetrics.Utils;
using MetricsFacade.Metric;

namespace miniMetrics.Metric
{
    public class TimeInterval : IHideObjectMembers, ITimeInterval
    {
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public long Duration => this.EndTime - this.StartTime;

        public long GetStartTime() { return this.StartTime; }
        public long GetEndTime() { return this.EndTime; }
        public long GetDuration() { return Duration; }

        public void Start()
        {
            this.StartTime = Clock.Default.Nanoseconds;
            this.EndTime = 0;
        } 

        public void Stop()
        {
            this.EndTime = Clock.Default.Nanoseconds;
        }

        public void Reset()
        {
            this.EndTime = 0;
            this.StartTime = 0;
        }
    }
}