using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

using miniMetrics.Collections;
using miniMetrics.Metric;

namespace miniMetrics.Test.Collections
{
    public class TimeIntervalCollectionTest
    {
        [Fact]
        public void TimeIntervalCollectionAcceptance()
        {
            var collection = new TimeIntervalCollection();
            Assert.Equal(0, collection.Count);

            collection.AddIntervals("i1");
            collection.AddIntervals("i2");
            Assert.Equal(2, collection.GetIntervalsCount());

            var ti = new TimeInterval();
            ti.Start();
            ti.Stop();

            collection.AddInterval("i1", ti);
            collection.AddInterval("i2", ti);

            collection.AddInterval("i3", ti);
            collection.AddInterval("i3", ti);

            var i3 = collection.GetIntervals("i3").ToList();
            Assert.Equal(2, i3.Count);

            collection.Reset("i3");
            i3 = collection.GetIntervals("i3").ToList();
            Assert.Equal(0, i3.Count);
        }
    }
}
