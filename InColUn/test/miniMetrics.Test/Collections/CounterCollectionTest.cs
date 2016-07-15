using miniMetrics.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace miniMetrics.Test.Collections
{
    public class CounterCollectionTest
    {
        [Fact]
        public void CounterCollectionAcceptance()
        {
            var collection = new CounterCollection();
            Assert.Equal(0, collection.Count);
            collection.AddCounter("test");
            Assert.Equal(1, collection.Count);
            //try to register metric with same name
            collection.AddCounter("test");
            Assert.Equal(1, collection.Count);

            collection.AddCounter("test1");
            Assert.Equal(2, collection.Count);

            collection["test"].SetValue(10);
            collection["test"].Increment();
            collection["test"].Increment();
            collection["test"].Decrement();
            collection["test1"] = 20;
            collection["test1"].Increment();

            Assert.Equal(11, collection["test"].Value);
            Assert.Equal(21, collection["test1"].Value);
        }
        
        [Fact]
        public async Task CounterCollectionMultiTask()
        {
            var counters = new CounterCollection();
            counters.AddCounter("test");
            counters.AddCounter("test1");

            var task1 = Task.Run( () => FillCollection(counters));
            var task2 = Task.Run( () => FillCollection(counters));

            await Task.WhenAll(task1, task2);

            var res1 = await task1;
            var res2 = await task2;
            Assert.Equal((res1 + res2), counters["test"].Value + counters["test1"].Value);
            Assert.Equal(counters["test"].Value, counters["test1"].Value);
        }

        private long FillCollection(CounterCollection counters)
        {
            var r = new Random();
            long cycles = 50 + r.Next() % 200;
            if (cycles % 2 == 1) cycles++;

            Enumerable.Range(1, (int)cycles).ToList()
                .ForEach(x =>
                {
                    if(x % 2 == 0)
                    {
                        counters["test"].Increment();
                    }
                    else
                    {
                        counters["test1"].Increment();
                    }
                });
            return cycles;
        }
    }
}
