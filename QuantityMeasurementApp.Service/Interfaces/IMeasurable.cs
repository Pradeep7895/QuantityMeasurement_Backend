namespace QuantityMeasurementApp.Service.Interfaces
{
    /// <summary>
    /// IMeasurable Interface
    /// UC-10
    /// </summary>
    public interface IMeasurable
    {
        /// <summary>
        /// Gets conversion factor to base unit
        /// </summary>
        double GetConversionFactor();

        /// <summary>
        /// Converts value to base unit
        /// </summary>
        double ConvertToBaseUnit(double value);

        /// <summary>
        /// Converts from base unit to this unit
        /// </summary>
        double ConvertFromBaseUnit(double baseValue);

        /// <summary>
        /// Gets the unit name
        /// </summary>
        string GetUnitName();

        /// <summary>
        /// Gets the measurement type (Length, Weight, Volume, Temperature)
        /// </summary>
        string GetMeasurementType();

        /// <summary>
        /// Default arithmetic support (true for most, false for Temperature)
        /// </summary>
        bool SupportsArithmetic();

        /// <summary>
        /// Validate operation support
        /// </summary>
        void ValidateOperationSupport(string operation);
    }
}