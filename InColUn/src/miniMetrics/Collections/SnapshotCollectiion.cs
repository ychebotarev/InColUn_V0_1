using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using miniMetrics.Metric;
using MetricsFacade.Collections;

namespace miniMetrics.Collections
{
    public class SnapshotCollectiion : Utils.IHideObjectMembers, ISnapshotCollectiion
    {
        private ConcurrentDictionary<string, ConcurrentBag<Snapshot>> snapshots = new ConcurrentDictionary<string, ConcurrentBag<Snapshot>>();

        public void AddSnapshot(string name)
        {
            if (this.snapshots.ContainsKey(name)) return;
            this.snapshots[name] = new ConcurrentBag<Snapshot>();
        }

        public void AddSnapshotValue(string name, double value)
        {
            this.snapshots[name].Add(new Snapshot(value));
        }

        public IEnumerable<Tuple<DateTime, double>> GetSnaphotValues(string name)
        {
            return this.snapshots[name].ToArray().Select(s => new Tuple<DateTime, double>(s.Date, s.Value));
        }
    }
}
