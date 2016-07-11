using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using miniMetrics.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace miniMetrics.Test.Collections
{
    [TestClass]
    public class CounterCollectionTest
    {
        [TestMethod]
        public void CounterCollectionAcceptance()
        {
            var collection = new CounterCollection();
            collection.Count.Should().Be(0);
            collection.Register("test");
            collection.Count.Should().Be(1);
            //try to register metric with same name
            collection.Register("test");
            collection.Count.Should().Be(1);

            collection.Register("test1");
            collection.Count.Should().Be(2);

            collection["test"].SetValue(10);
            collection["test"].Increment();
            collection["test"].Increment();
            collection["test"].Decrement();
            collection["test1"] = 20;
            collection["test1"].Increment();

            collection["test"].Value.Should().Be(11);
            collection["test1"].Value.Should().Be(21);
        }
        
        [TestMethod]
        public async Task CounterCollectionMultiTask()
        {
            var counters = new CounterCollection();
            counters.Register("test");
            counters.Register("test1");

            var task1 = Task.Run( () => FillCollection(counters));
            var task2 = Task.Run( () => FillCollection(counters));

            await Task.WhenAll(task1, task2);

            var res1 = await task1;
            var res2 = await task2;

            (res1 + res2).Should().Be(counters["test"].Value + counters["test1"].Value);
            counters["test"].Value.Should().Be(counters["test1"].Value);
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
