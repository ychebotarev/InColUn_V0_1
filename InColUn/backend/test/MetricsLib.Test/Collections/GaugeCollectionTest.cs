using Xunit;

using miniMetrics.Collections;

namespace miniMetrics.Test.Collections
{
    public class GaugeCollectionTest
    {
        [Fact]
        public void GaugeCollectionAcceptance()
        {
            var collection = new GaugeCollection();
            Assert.Equal(0, collection.Count);

            collection.AddGauge("g1");
            Assert.Equal(1, collection.Count);
            Assert.Equal(0.0, collection.GetGauge("g1"));
            Assert.Equal(0.0, collection["g1"]);

            collection.AddGauge("g2");
            Assert.Equal(2, collection.GetGaugessCount());
            collection["g1"] = 10.0;
            collection["g2"] = 20.0;

            Assert.Equal(30.0, collection["g1"] + collection.GetGauge("g2"));
        }
    }
}
