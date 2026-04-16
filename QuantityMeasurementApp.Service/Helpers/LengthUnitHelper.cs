using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Service.Helpers
{
    /// <summary>
    /// LengthUnitHelper
    /// UC-10
    /// </summary>
    public class LengthUnitHelper : IMeasurable
    {
        // Holds the length unit enum value (FEET, INCH, YARD, CENTIMETERS)
        private readonly LengthUnit unit;

        // Constructor that initializes the helper with a specific length unit
        public LengthUnitHelper(LengthUnit unit)
        {
            this.unit = unit;
        }

        // Returns conversion factor relative to base unit (INCH)
        // FEET = 12 inches, INCH = 1 inch (base), YARD = 36 inches, CENTIMETERS = 0.393701 inches
        public double GetConversionFactor()
        {
            return unit switch
            {
                LengthUnit.FEET => 12.0,
                LengthUnit.INCH => 1.0,
                LengthUnit.YARD => 36.0,
                LengthUnit.CENTIMETERS => 0.393701,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        // Converts the given value to base unit (INCH) by multiplying with conversion factor
        public double ConvertToBaseUnit(double value)
        {
            Validate(value);
            return value * GetConversionFactor();
        }

        // Converts from base unit (INCH) to this unit by dividing by conversion factor
        // Result is rounded to 2 decimal places for readability
        public double ConvertFromBaseUnit(double baseValue)
        {
            Validate(baseValue);
            return Math.Round(baseValue / GetConversionFactor(), 2);
        }

        // Returns the string representation of the unit (e.g., "FEET", "INCH")
        public string GetUnitName()
        {
            return unit.ToString();
        }

        // Returns the measurement type as string - used for category identification
        public string GetMeasurementType()
        {
            return "Length";
        }

        // Indicates whether this unit supports arithmetic operations
        // Length units support addition, subtraction, and division
        public bool SupportsArithmetic()
        {
            return true;
        }

        // Validates if a specific operation is supported by this unit
        // Length supports all operations, so this method does nothing
        public void ValidateOperationSupport(string operation)
        {
            // Length supports all operations
        }

        // Private helper method to validate input values
        private void Validate(double value)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid measurement value.");
        }
    }
}