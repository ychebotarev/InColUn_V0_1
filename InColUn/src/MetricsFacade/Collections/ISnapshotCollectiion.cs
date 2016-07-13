using System;
using System.Collections.Generic;

namespace MetricsFacade.Collections
{
    public interface ISnapshotCollectiion
    {
        void AddSnapshot(string name);
        void AddSnapshotValue(string name, double value);
        IEnumerable<Tuple<DateTime, double>> GetSnaphotValues(string name);
    }
}
