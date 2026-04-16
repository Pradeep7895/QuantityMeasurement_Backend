using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Service.Helpers;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Mappers;

namespace QuantityMeasurementApp.Service.Services
{
    /// <summary>
    /// Service for conversion operations using YOUR helper classes
    /// Handles unit-to-unit conversions for all measurement types
    /// </summary>
    public class ConversionService : IConversionService
    {
        /// <summary>
        /// Converts a quantity from its current unit to a target unit
        /// </summary>
        /// <param name="source">The source quantity to convert</param>
        /// <param name="targetUnit">The target unit for conversion</param>
        /// <returns>A new QuantityDTO with converted value and target unit</returns>
        public QuantityDTO Convert(QuantityDTO source, string targetUnit)
        {
            // Create quantity using your helper classes
            var quantity = QuantityMapper.ToQuantity(source);
            
            // Create target unit helper
            var targetMeasurable = CreateMeasurable(targetUnit, source.MeasurementType);
            
            // Use your ConvertTo logic (preserved from your original code)
            // Step 1: Convert source value to base unit
            double baseValue = quantity.Unit.ConvertToBaseUnit(quantity.Value);
            
            // Step 2: Convert from base unit to target unit
            double convertedValue = targetMeasurable.ConvertFromBaseUnit(baseValue);
            
            // Return new DTO with converted value (rounded to 2 decimal places)
            return new QuantityDTO
            {
                Value = Math.Round(convertedValue, 2),
                Unit = targetUnit,
                MeasurementType = source.MeasurementType
            };
        }

        /// <summary>
        /// Converts a quantity to its base unit value
        /// </summary>
        /// <param name="quantity">The quantity to convert</param>
        /// <returns>The value in base unit (inches for length, grams for weight, etc.)</returns>
        public double ToBaseUnit(QuantityDTO quantity)
        {
            // Create measurable helper for the unit
            var measurable = CreateMeasurable(quantity.Unit, quantity.MeasurementType);
            // Convert to base unit
            return measurable.ConvertToBaseUnit(quantity.Value);
        }

        /// <summary>
        /// Creates a quantity from a base unit value
        /// </summary>
        /// <param name="baseValue">The value in base unit</param>
        /// <param name="targetUnit">The target unit for the result</param>
        /// <param name="measurementType">The type of measurement</param>
        /// <returns>A new QuantityDTO with value converted from base to target unit</returns>
        public QuantityDTO FromBaseUnit(double baseValue, string targetUnit, string measurementType)
        {
            // Create measurable helper for target unit
            var measurable = CreateMeasurable(targetUnit, measurementType);
            // Convert from base unit to target unit
            double convertedValue = measurable.ConvertFromBaseUnit(baseValue);
            
            // Return new DTO with converted value (rounded to 2 decimal places)
            return new QuantityDTO
            {
                Value = Math.Round(convertedValue, 2),
                Unit = targetUnit,
                MeasurementType = measurementType
            };
        }

        /// <summary>
        /// Private helper method to create the appropriate IMeasurable helper
        /// </summary>
        /// <param name="unitName">The name of the unit</param>
        /// <param name="measurementType">The type of measurement</param>
        /// <returns>An IMeasurable helper instance</returns>
        private IMeasurable CreateMeasurable(string unitName, string measurementType)
        {
            return measurementType switch
            {
                "Length" => new LengthUnitHelper(Enum.Parse<LengthUnit>(unitName)),
                "Weight" => new WeightUnitHelper(Enum.Parse<WeightUnit>(unitName)),
                "Volume" => new VolumeUnitHelper(Enum.Parse<VolumeUnit>(unitName)),
                "Temperature" => new TemperatureUnitHelper(Enum.Parse<TemperatureUnit>(unitName)),
                _ => throw new ArgumentException($"Unknown measurement type: {measurementType}")
            };
        }
    }
}