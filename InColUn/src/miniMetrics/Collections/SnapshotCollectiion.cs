using System.Collections.Concurrent;
using miniMetrics.Metric;

namespace miniMetrics.Collections
{
    class SnapshotCollectiion : Utils.IHideObjectMembers
    {
        private ConcurrentDictionary<string, ConcurrentBag<Snapshot>> _snapshots = new ConcurrentDictionary<string, ConcurrentBag<Snapshot>>();

        public void Register(string name)
        {
            if (this._snapshots.ContainsKey(name)) return;
            this._snapshots[name] = new ConcurrentBag<Snapshot>();
        }

        public void Add(string name, double value)
        {
            this._snapshots[name].Add(new Snapshot(value));
        }
    }
}
