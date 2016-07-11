using System;
namespace Metrics.MetricData
{
    public sealed class ScaledValueProvider<T> : MetricValueProvider<T>
    {
        private readonly MetricValueProvider<T> valueProvider;
        private readonly Func<T, T> scalingFunction;

        public ScaledValueProvider(MetricValueProvider<T> valueProvider, Func<T, T> transformation)
        {
            this.valueProvider = valueProvider;
            this.scalingFunction = transformation;
        }

        public T Value
        {
            get
            {
                return this.scalingFunction(this.valueProvider.Value);
            }
        }

        public T GetValue(bool resetMetric = false)
        {
            return this.scalingFunction(this.valueProvider.GetValue(resetMetric));
        }

        public MetricValueProvider<T> ValueProvider
        {
            get { return this.valueProvider; }
        }
    }
}