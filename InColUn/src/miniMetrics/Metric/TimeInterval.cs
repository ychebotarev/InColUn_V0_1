using miniMetrics.Utils;
using System;
using System.Collections.Concurrent;

namespace miniMetrics.Metric
{
    public class TimeInterval : IHideObjectMembers
    {
        public ConcurrentBag<Tuple<long,long>> ticks;

        public TimeInterval()
        {
            this.ticks = new ConcurrentBag<Tuple<long, long>>();
        }

        public long StartTime { get; private set; }
        public long StopTime { get; private set; }
        public long Duration => this.StopTime - this.StartTime;
          
        public void Start()
        {
            this.StartTime = Clock.Default.Nanoseconds;
            this.StopTime = 0;
        } 

        public void Stop()
        {
            this.StopTime = Clock.Default.Nanoseconds;
            this.ticks.Add(new Tuple<long, long>(this.StartTime, this.StopTime));
        }

        public void Reset()
        {
            this.StopTime = 0;
            this.StartTime = 0;
            this.ticks = new ConcurrentBag<Tuple<long, long>>();
        }
    }
}