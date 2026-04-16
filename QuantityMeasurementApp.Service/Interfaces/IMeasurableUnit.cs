namespace QuantityMeasurementApp.Service.Interfaces
{
    /// <summary>
    /// Interface for enum units
    /// </summary>
    public interface IMeasurableUnit
    {
        /// <summary>
        /// Gets the unit name as string
        /// </summary>
        string GetUnitName();

        /// <summary>
        /// Gets the measurement type
        /// </summary>
        string GetMeasurementType();

        /// <summary>
        /// Creates appropriate IMeasurable helper from unit
        /// </summary>
        IMeasurable CreateMeasurable();
    }
}