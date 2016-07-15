using System;
using System.Linq;
using miniMetrics.Metric;
using Xunit;

namespace miniMetrics.Test.SingleValue
{
    public class MeterRateTest
    {
        [Fact]
        public void RateTestAcceptance()
        {
            var rate = Meter.createM1Rate();
            rate.Update(10);
            rate.Tick();
            Assert.Equal(10, rate.Value);

            rate.Update(10);
            rate.Tick();
            Assert.Equal(10, rate.Value);

            Enumerable.Range(0, 10).ToList().ForEach(x => rate.Increment());
            rate.Tick();
            Assert.Equal(10, rate.Value);

            Enumerable.Range(0, 10).ToList().ForEach(x => rate.Increment());
            rate.Update(10);
            rate.Tick();
            Assert.True(rate.Value > 10);

            rate = Meter.createM1Rate();
            rate.Update(10);
            rate.Tick();
            Enumerable.Range(0, 60).ToList().ForEach(x => { rate.Update(100); rate.Tick(); });
            Assert.True(rate.Value > 10);
            Assert.True(rate.Value > 99);

            rate = Meter.createM5Rate();
            rate.Update(10);
            rate.Tick();
            Enumerable.Range(0, 60 * 5).ToList().ForEach(x => { rate.Update(100); rate.Tick(); });
            Assert.True(rate.Value > 10);
            Assert.True(rate.Value > 99);

            rate = Meter.createM15Rate();
            rate.Update(10);
            rate.Tick();
            Enumerable.Range(0, 60 * 15).ToList().ForEach(x => { rate.Update(100); rate.Tick(); });
            Assert.True(rate.Value > 10);
            Assert.True(rate.Value > 99);
        }
    }
}
