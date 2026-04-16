
namespace QuantityMeasurementApp.Model.DTOs
{
    /// <summary>
    /// Generic class that holds quantity data with strongly-typed unit
    /// </summary>
    public class QuantityModel<T>
    {
        /// <summary>
        /// Gets or sets the numeric value of the quantity
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the unit of measurement
        /// </summary>
        public T Unit { get; set; }

        /// <summary>
        /// Default constructor for QuantityModel
        /// </summary>
        public QuantityModel()
        {
        }

        /// <summary>
        /// Parameterized constructor for QuantityModel
        /// </summary>
        public QuantityModel(double value, T unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}