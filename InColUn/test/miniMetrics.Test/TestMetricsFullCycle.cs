using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using miniMetrics.Collections;
using miniMetrics.Metric;

using Xunit;

namespace miniMetrics.Test
{
    public class TestMetricsFullCycle
    {
        [Fact]
        public async Task MetricsCombinedAcceptanceTest()
        {
            var metrics = new MetricsService("test");

            metrics.Counters.AddCounter("test.counters.one");
            metrics.Counters.AddCounter("test.counters.two");

            metrics.Rates.AddMeter("test.rate.one");
            metrics.Rates.AddMeter("test.rate.two");

            metrics.Snapshots.AddSnapshot("test.snapshot.one");
            metrics.Snapshots.AddSnapshot("test.snapshot.two");

            metrics.Intervals.AddIntervals("test.intervals.one");
            metrics.Intervals.AddIntervals("test.intervals.two");

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
            Assert.Equal(count1 + count2
                , metrics.Counters["test.counters.two"].Value + metrics.Counters["test.counters.one"].Value);

            Assert.Equal(metrics.Counters["test.counters.two"].Value, metrics.Counters["test.counters.one"].Value);

        }

        private long GenerateIntervals(MetricsService metrics, string name)
        {
            var r = new Random();
            long cycles = 50 + r.Next() % 200;
            if (cycles % 2 == 1) cycles++;
            Enumerable.Range(1, (int)cycles).ToList()
                .ForEach(x =>
                {
                    var ti = new TimeInterval();
                    ti.Start();
                    Thread.Sleep(10);
                    ti.Stop();
                    metrics.Intervals.AddInterval(name, ti);
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

            var rates = metrics.Rates as MeterCollection;

            rates.Stop(MeterIntervals.oneMin);
            rates.Stop(MeterIntervals.tenSec);

            Enumerable.Range(1, (int)cycles).ToList()
                .ForEach(x =>
                {
                    if (x % 3 == 0)
                    {
                        rates[name1].Increment();
                    }
                    else
                    {
                        metrics.Rates[name2].Increment();
                    }
                });

            rates[MeterIntervals.oneMin].Tick();
            rates[MeterIntervals.tenSec].Tick();

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


            rates[MeterIntervals.oneMin].Tick();
            rates[MeterIntervals.tenSec].Tick();

            return cycles;
        }
    }
}