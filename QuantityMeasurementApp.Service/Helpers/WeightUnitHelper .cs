using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Service.Helpers
{
    /// <summary>
    /// WeightUnitHelper 
    /// UC-10
    /// </summary>
    public class WeightUnitHelper : IMeasurable
    {
        // Holds the weight unit enum value (MILLIGRAM, GRAM, KILOGRAM, POUND, TONNE)
        private readonly WeightUnit unit;

        // Constructor that initializes the helper with a specific weight unit
        public WeightUnitHelper(WeightUnit unit)
        {
            this.unit = unit;
        }

        // Returns conversion factor relative to base unit (GRAM)
        // MILLIGRAM = 0.001 (1 mg = 0.001 g)
        // GRAM = 1.0 (base unit)
        // KILOGRAM = 1000.0 (1 kg = 1000 g)
        // POUND = 453.592 (1 lb = 453.592 g)
        // TONNE = 1000000.0 (1 tonne = 1,000,000 g)
        public double GetConversionFactor()
        {
            return unit switch
            {
                WeightUnit.MILLIGRAM => 0.001,
                WeightUnit.GRAM => 1.0,
                WeightUnit.KILOGRAM => 1000.0,
                WeightUnit.POUND => 453.592,
                WeightUnit.TONNE => 1000000.0,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        // Converts the given value to base unit (GRAM) by multiplying with conversion factor
        public double ConvertToBaseUnit(double value)
        {
            Validate(value);
            return value * GetConversionFactor();
        }

        // Converts from base unit (GRAM) to this unit by dividing by conversion factor
        // Result is rounded to 2 decimal places for readability
        public double ConvertFromBaseUnit(double baseValue)
        {
            Validate(baseValue);
            return Math.Round(baseValue / GetConversionFactor(), 2);
        }

        // Returns the string representation of the unit (e.g., "KILOGRAM", "GRAM", "POUND")
        public string GetUnitName()
        {
            return unit.ToString();
        }

        // Returns the measurement type as string - used for category identification
        public string GetMeasurementType()
        {
            return "Weight";
        }

        // Indicates whether this unit supports arithmetic operations
        // Weight units support addition, subtraction, and division
        public bool SupportsArithmetic()
        {
            return true;
        }

        // Validates if a specific operation is supported by this unit
        // Weight supports all operations, so this method does nothing
        public void ValidateOperationSupport(string operation)
        {
            // Weight supports all operations
        }

        // Private helper method to validate input values
        private void Validate(double value)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid measurement value.");
        }
    }
}