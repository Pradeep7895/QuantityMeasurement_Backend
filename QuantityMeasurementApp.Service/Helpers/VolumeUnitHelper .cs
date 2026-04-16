using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Service.Helpers
{
    /// <summary>
    /// VolumeUnitHelper
    /// UC-11
    /// </summary>
    public class VolumeUnitHelper : IMeasurable
    {
        // Holds the volume unit enum value (LITRE, MILLILITRE, GALLON)
        private readonly VolumeUnit unit;

        // Constructor that initializes the helper with a specific volume unit
        public VolumeUnitHelper(VolumeUnit unit)
        {
            this.unit = unit;
        }

        // Returns conversion factor relative to base unit (LITRE)
        // LITRE = 1.0 (base unit)
        // MILLILITRE = 0.001 (1 mL = 0.001 L)
        // GALLON = 3.78541 (1 gallon = 3.78541 litres)
        public double GetConversionFactor()
        {
            return unit switch
            {
                VolumeUnit.LITRE => 1.0,
                VolumeUnit.MILLILITRE => 0.001,
                VolumeUnit.GALLON => 3.78541,
                _ => throw new ArgumentException("Unsupported volume unit")
            };
        }

        // Converts the given value to base unit (LITRE) by multiplying with conversion factor
        public double ConvertToBaseUnit(double value)
        {
            Validate(value);
            return value * GetConversionFactor();
        }

        // Converts from base unit (LITRE) to this unit by dividing by conversion factor
        public double ConvertFromBaseUnit(double baseValue)
        {
            Validate(baseValue);
            return baseValue / GetConversionFactor();
        }

        // Returns the string representation of the unit (e.g., "LITRE", "MILLILITRE")
        public string GetUnitName()
        {
            return unit.ToString();
        }

        // Returns the measurement type as string - used for category identification
        public string GetMeasurementType()
        {
            return "Volume";
        }

        // Indicates whether this unit supports arithmetic operations
        // Volume units support addition, subtraction, and division
        public bool SupportsArithmetic()
        {
            return true;
        }

        // Validates if a specific operation is supported by this unit
        // Volume supports all operations, so this method does nothing
        public void ValidateOperationSupport(string operation)
        {
            // Volume supports all operations
        }

        // Private helper method to validate input values
        private void Validate(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");
        }

        // Returns string representation of the unit
        public override string ToString()
        {
            return unit.ToString();
        }
    }
}