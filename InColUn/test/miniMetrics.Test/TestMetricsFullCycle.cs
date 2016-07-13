using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using miniMetrics.Collections;
using FluentAssertions;

namespace miniMetrics.Test
{
    /// <summary>
    /// Summary description for TestMetricsFullCycle
    /// </summary>
    [TestClass]
    public class TestMetricsFullCycle
    {
        public TestMetricsFullCycle()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public async Task MultiTask()
        {
            var metrics = new MetricsService("test");

            metrics.Counters.AddCounter("test.counters.one");
            metrics.Counters.AddCounter("test.counters.two");

            metrics.Rates.AddMeter("test.rate.one");
            metrics.Rates.AddMeter("test.rate.two");

            metrics.Snapshots.AddSnapshot("test.snapshot.one");
            metrics.Snapshots.AddSnapshot("test.snapshot.two");

            metrics.Intervals.AddInterval("test.intervals.one");
            metrics.Intervals.AddInterval("test.intervals.two");

            //fill values
            var taskCounters1 = Task.Run( () => GenerateCounters(metrics, "test.counters.one", "test.counters.two"));
            var taskCounters2 = Task.Run(() => GenerateCounters(metrics, "test.counters.two", "test.counters.one"));

            var taskRate1 = Task.Run(() => GenerateRates(metrics, "test.rate.one", "test.rate.two"));
            var taskRate2 = Task.Run(() => GenerateRates(metrics, "test.rate.two", "test.rate.one"));

            var taskSnapshot1 = Task.Run(() => GenerateSnapshots(metrics, "test.snapshot.one", "test.snapshot.two"));
            var taskSnapshot2 = Task.Run(() => GenerateSnapshots(metrics, "test.snapshot.two", "test.snapshot.one"));

            var taskIntervals1 = Task.Run(() => GenerateIntervals(metrics, "test.intervals.one"));
            var taskIntervals2 = Task.Run(() => GenerateIntervals(metrics, "test.intervals.two"));

            await Task.WhenAll(new Task[]
            {
                taskCounters1, taskCounters2, 
                taskRate1, taskRate2, 
                taskSnapshot1, taskSnapshot2, 
                taskIntervals1, taskIntervals2
            });

            var count1 = await taskCounters1;
            var count2 = await taskCounters2;
            (count1 + count2).Should()
                .Be(metrics.Counters["test.counters.two"].Value + metrics.Counters["test.counters.one"].Value);

            metrics.Counters["test.counters.two"].Value.Should()
                .Be(metrics.Counters["test.counters.one"].Value);

        }

        private long GenerateIntervals(MetricsService metrics, string name)
        {
            var r = new Random();
            long cycles = 50 + r.Next() % 200;
            if (cycles % 2 == 1) cycles++;
            Enumerable.Range(1, (int)cycles).ToList()
                .ForEach(x =>
                {
                    metrics.Intervals[name].Start();
                    Thread.Sleep(10);
                    metrics.Intervals[name].Stop();
                });
            return cycles;
        }

        private long GenerateSnapshots(MetricsService metrics, string name1, string name2)
        {
            var r = new Random();
            long cycles = 50 + r.Next() % 200;
            if (cycles % 2 == 1) cycles++;
            Enumerable.Range(1, (int)cycles).ToList()
                .ForEach(x =>
                {
                    if (x % 2 == 0)
                    {
                        metrics.Snapshots.AddSnapshotValue(name1, x);
                    }
                    else
                    {
                        metrics.Snapshots.AddSnapshotValue(name2, x);
                    }
                });
            return cycles;
        }

        private long GenerateCounters(MetricsService metrics, string name1, string name2)
        {
            var r = new Random();
            long cycles = 50 + r.Next() % 200;
            while(cycles % 3 != 0) cycles++;

            Enumerable.Range(1, (int)cycles).ToList()
                .ForEach(x =>
                {
                    if (x % 3 == 0)
                    {
                        metrics.Counters[name1].Increment();
                    }
                    else
                    {
                        metrics.Counters[name2].Increment();
                    }
                });
            return cycles;
        }

        private long GenerateRates(MetricsService metrics, string name1, string name2)
        {
            var r = new Random();
            long cycles = 50 + r.Next() % 200;
            if (cycles % 2 == 1) cycles++;

            metrics.Rates.Stop(MeterIntervals.oneMin);
            metrics.Rates.Stop(MeterIntervals.tenSec);

            Enumerable.Range(1, (int)cycles).ToList()
                .ForEach(x =>
                {
                    if (x % 3 == 0)
                    {
                        metrics.Rates[name1].Increment();
                    }
                    else
                    {
                        metrics.Rates[name2].Increment();
                    }
                });

            metrics.Rates[MeterIntervals.oneMin].Tick();
            metrics.Rates[MeterIntervals.tenSec].Tick();

            Enumerable.Range(1, (int)cycles * 5).ToList()
                .ForEach(x =>
                {
                    if (x % 3 == 0)
                    {
                        metrics.Rates[name1].Increment();
                    }
                    else
                    {
                        metrics.Rates[name2].Increment();
                    }
                });


            metrics.Rates[MeterIntervals.oneMin].Tick();
            metrics.Rates[MeterIntervals.tenSec].Tick();

            return cycles;
        }
    }
}