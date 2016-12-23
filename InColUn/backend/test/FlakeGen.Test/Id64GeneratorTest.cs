using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace FlakeGen.Test
{
    public class Id64GeneratorTest
    {
        private const int HowManyIds = 10000;
        private const int HowManyThreads = 10;

        [Fact]
        public void UniqueLongIds()
        {
            IIdGenerator<long> idGenerator = new Id64Generator();

            long[] ids = idGenerator.Take(HowManyIds).ToArray();

            Assert.True(AssertUtil.AreUnique(ids), "All ids needs to be unique");
        }

        [Fact]
        public void SortableLongIds()
        {
            IIdGenerator<long> idGenerator = new Id64Generator();

            long[] ids = idGenerator.Take(HowManyIds).ToArray();

            Assert.True(AssertUtil.AreSorted(ids), "Ids array needs to be ordered");
        }

        [Fact]
        public void DistinctLongIdsForMultiThreads()
        {
            Thread[] threads = new Thread[HowManyThreads];
            long[][] ids = new long[HowManyThreads][];
            List<long> allIds = new List<long>(HowManyIds * HowManyThreads);

            IIdGenerator<long> idGenerator = new Id64Generator();

            for (int i = 0; i < HowManyThreads; i++)
            {
                var threadId = i;
                threads[i] = new Thread(() =>
                {
                    ids[threadId] = idGenerator.Take(HowManyIds).ToArray();
                });
                threads[i].Start();
            }

            for (int i = 0; i < HowManyThreads; i++)
            {
                threads[i].Join();

                Assert.True(AssertUtil.AreUnique(ids[i]), "All ids needs to be unique");
                Assert.True(AssertUtil.AreSorted(ids[i]), "Ids array needs to be ordered");

                allIds.AddRange(ids[i]);
            }

            Assert.Equal(HowManyIds * HowManyThreads, allIds.Distinct().Count());
        }
    }
}
