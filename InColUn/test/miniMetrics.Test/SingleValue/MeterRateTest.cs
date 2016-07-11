using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using miniMetrics.Metric;
using FluentAssertions;

namespace miniMetrics.Test.SingleValue
{
    [TestClass]
    public class MeterRateTest
    {
        [TestMethod]
        public void RateTestAcceptance()
        {
            var rate = Meter.createM1Rate();
            rate.Update(10);
            rate.Tick();
            rate.Value.Should().Be(10);

            rate.Update(10);
            rate.Tick();
            rate.Value.Should().Be(10);

            Enumerable.Range(0, 10).ToList().ForEach(x => rate.Increment());
            rate.Tick();
            rate.Value.Should().Be(10);

            Enumerable.Range(0, 10).ToList().ForEach(x => rate.Increment());
            rate.Update(10);
            rate.Tick();
            rate.Value.Should().BeGreaterThan(10);

            rate = Meter.createM1Rate();
            rate.Update(10);
            rate.Tick();
            Enumerable.Range(0, 60).ToList().ForEach(x => { rate.Update(100); rate.Tick(); });
            rate.Value.Should().BeGreaterThan(10);
            rate.Value.Should().BeGreaterThan(99);

            rate = Meter.createM5Rate();
            rate.Update(10);
            rate.Tick();
            Enumerable.Range(0, 60 * 5).ToList().ForEach(x => { rate.Update(100); rate.Tick(); });
            rate.Value.Should().BeGreaterThan(10);
            rate.Value.Should().BeGreaterThan(99);

            rate = Meter.createM15Rate();
            rate.Update(10);
            rate.Tick();
            Enumerable.Range(0, 60 * 15).ToList().ForEach(x => { rate.Update(100); rate.Tick(); });
            rate.Value.Should().BeGreaterThan(10);
            rate.Value.Should().BeGreaterThan(99);

        }
    }
}
