using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Service.Helpers
{
    /// <summary>
    /// TemperatureUnitHelper 
    /// UC-14
    /// </summary>
    public class TemperatureUnitHelper : IMeasurable
    {
        // Holds the temperature unit enum value (CELSIUS, FAHRENHEIT, KELVIN)
        private readonly TemperatureUnit unit;

        // Constructor that initializes the helper with a specific temperature unit
        public TemperatureUnitHelper(TemperatureUnit unit)
        {
            this.unit = unit;
        }

        // Returns the string representation of the unit (e.g., "CELSIUS", "FAHRENHEIT")
        public string GetUnitName()
        {
            return unit.ToString();
        }

        // Returns the measurement type as string - used for category identification
        public string GetMeasurementType()
        {
            return "Temperature";
        }

        // Returns conversion factor - always 1.0 for temperature
        public double GetConversionFactor()
        {
            return 1.0;
        }

        // Converts the given value to base unit (Celsius)
        // Uses specific formulas for each temperature unit:
        // Celsius -> Celsius: direct value
        // Fahrenheit -> Celsius: (°F - 32) × 5/9
        // Kelvin -> Celsius: K - 273.15
        public double ConvertToBaseUnit(double value)
        {
            // Base unit = Celsius
            switch (unit)
            {
                case TemperatureUnit.CELSIUS:
                    return value;

                case TemperatureUnit.FAHRENHEIT:
                    return (value - 32) * 5 / 9;

                case TemperatureUnit.KELVIN:
                    return value - 273.15;

                default:
                    throw new InvalidOperationException("Unsupported temperature unit");
            }
        }

        // Converts from base unit (Celsius) to this unit
        // Uses specific formulas for each temperature unit:
        // Celsius -> Celsius: direct value
        // Celsius -> Fahrenheit: (°C × 9/5) + 32
        // Celsius -> Kelvin: °C + 273.15
        public double ConvertFromBaseUnit(double baseValue)
        {
            switch (unit)
            {
                case TemperatureUnit.CELSIUS:
                    return baseValue;

                case TemperatureUnit.FAHRENHEIT:
                    return (baseValue * 9 / 5) + 32;

                case TemperatureUnit.KELVIN:
                    return baseValue + 273.15;

                default:
                    throw new InvalidOperationException("Unsupported temperature unit");
            }
        }

        // Indicates whether this unit supports arithmetic operations
        // Temperature units do NOT support addition, subtraction, or division
        public bool SupportsArithmetic()
        {
            return false;
        }

        // Validates if a specific operation is supported by this unit
        // Temperature does not support any arithmetic operations
        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException(
                $"Temperature does not support {operation} operations.");
        }
    }
}