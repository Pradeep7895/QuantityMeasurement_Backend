

namespace QuantityMeasurementApp.Model.Entities
{
    /// <summary>
    /// Generic Quantity class 
    /// UC-10 Base Structure
    /// </summary>
    public class Quantity<T>
    {
        /// <summary>
        /// The numeric value of the quantity
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// The unit of measurement (implements IMeasurable)
        /// </summary>
        public T Unit { get; }

        /// <summary>
        /// Constructor - only initialization
        /// </summary>
        public Quantity(double value, T unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Returns string representation
        /// </summary>
        public override string ToString()
        {
            return $"{Value:F2} {Unit}";
        }
    }
}