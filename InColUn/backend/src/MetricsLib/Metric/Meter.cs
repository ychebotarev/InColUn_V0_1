using System;
using ConcurrencyUtilities;
using MetricsFacade.Metric;

namespace miniMetrics.Metric
{
    /*
    current rate will be measured per tick 

    If current rate is X, and target rate (real rate) is 100*X
    it will take roughtly 60 iterations 
    for current rate to reach target rate for M1_ALPHA

    */
    public class Meter : IMeter
    {
        public static readonly double M1_ALPHA = 1.0 - Math.Exp((double)-5 / 60);
        public static readonly double M5_ALPHA = 1.0 - Math.Exp((double)-5 / 60 / 5);
        public static readonly double M15_ALPHA = 1.0 - Math.Exp((double)-5 / 60 / 15);

        private double alpha;
        private double currentRate;
        private AtomicLong uncounted;

        public Meter(double alpha)
        {
            this.alpha = alpha;
            this.currentRate = -1;
            this.uncounted.SetValue(0);
        }

        public void Update(long n)
        {
            this.uncounted.Increment(n);
        }

        public void Increment()
        {
            this.uncounted.Increment();
        }

        public void Mark()
        {
            this.uncounted.Increment();
        }

        public void Tick()
        {
            var tickRate = this.uncounted.GetAndSet(0);

            if (this.currentRate != -1)
            {
                this.currentRate += this.alpha * (tickRate - this.currentRate);
            }
            else
            {
                this.currentRate = tickRate;
            }
        }

        public double Value => this.currentRate;
        public double GetRate() => this.currentRate;

        public static Meter createM1Rate() { return new Meter(M1_ALPHA); }
        public static Meter createM5Rate() { return new Meter(M5_ALPHA); }
        public static Meter createM15Rate() { return new Meter(M15_ALPHA); }
    }
}
