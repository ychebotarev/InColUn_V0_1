using ConcurrencyUtilities;

namespace miniMetrics.Metric
{
    class Counter: Utils.IHideObjectMembers
    {
        private AtomicLong _value;

        public Counter() { this._value.SetValue(0); }
        public Counter(long value) { this._value.SetValue(value); }
        public Counter(Counter c) { this._value = c._value; }

        public void Increment() { this._value.Increment(); }
        public void Increment(long step) { this._value.Increment(step); }
        public void Decrement() { this._value.Decrement(); }
        public void Decrement(long step) { this._value.Decrement(step);  }
        public void SetValue(long value) { this._value.SetValue(value); }

        public long Value => this._value.GetValue();

        public static implicit operator long(Counter c)
        {
            return c.Value;
        }

        public static implicit operator Counter(long d)
        {
            return new Counter(d);
        }
    }
}
