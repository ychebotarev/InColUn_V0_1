using System;
namespace Metrics.MetricData
{
    /// <summary>
    /// Provides the value of a metric and information about units.
    /// This is the class that metric consumers should use.
    /// </summary>
    /// <typeparam name="T">Type of the metric value</typeparam>
    public abstract class MetricValueSource<T> : Utils.IHideObjectMembers
    {
        protected MetricValueSource(string name, MetricValueProvider<T> valueProvider)
        {
            this.Name = name;
            this.ValueProvider = valueProvider;
        }

        /// <summary>
        /// Name of the metric.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The current value of the metric.
        /// </summary>
        public T Value { get { return this.ValueProvider.Value; } }


        /// <summary>
        /// Instance capable of returning the current value for the metric.
        /// </summary>
        public MetricValueProvider<T> ValueProvider { get; }
    }
}
