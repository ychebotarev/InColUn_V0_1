using System.Linq;
using Xunit;

using miniMetrics.Collections;

namespace miniMetrics.Test.Collections
{
    public class SnapshotCollectiionTest
    {
        [Fact]
        public void SnapshotCollectionAcceptance()
        {
            var collection = new SnapshotCollectiion();
            Assert.Equal(0, collection.Count);
            collection.AddSnapshot("s1");
            Assert.Equal(1, collection.GetSnapshotCount());

            collection.AddSnapshot("s1");
            Assert.Equal(2, collection.GetSnapshotCount());

            var s1 = collection.GetSnaphotValues("s1").ToList();
            Assert.Equal(0, s1.Count);

            collection.AddSnapshotValue("s1", 1);
            collection.AddSnapshotValue("s1", 2);

            s1 = collection.GetSnaphotValues("s1").ToList();
            Assert.Equal(2, s1.Count);

            Assert.Equal(1, s1.Count( x => x.Item2 == 1));
            Assert.Equal(1, s1.Count(x => x.Item2 == 2));
        }
    }
}
