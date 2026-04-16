namespace QuantityMeasurementApp.Model.DTOs
{
    /// <summary>
    /// Data Transfer Object for quantity operations
    /// Used to transfer quantity data between different layers of the application
    /// </summary>
    public class QuantityDTO
    {
        /// <summary>
        /// Gets or sets the numeric value of the quantity
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the unit of measurement as string (e.g., "FEET", "KILOGRAM", "LITRE")
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the type of measurement (e.g., "Length", "Weight", "Volume", "Temperature")
        /// </summary>
        public string MeasurementType { get; set; }

        /// <summary>
        /// Default constructor for QuantityDTO
        /// </summary>
        public QuantityDTO()
        {
        }

        /// <summary>
        /// Parameterized constructor for QuantityDTO
        /// </summary>
        public QuantityDTO(double value, string unit, string measurementType)
        {
            Value = value;
            Unit = unit;
            MeasurementType = measurementType;
        }

        /// <summary>
        /// Returns a string representation of the quantity
        /// </summary>
        public override string ToString()
        {
            return $"{Value:F2} {Unit}";
        }
    }
}