using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Service.Helpers;

namespace QuantityMeasurementApp.Service.Services
{
    /// <summary>
    /// Service for validation operations
    /// Handles validation of quantity DTOs and arithmetic operation feasibility
    /// </summary>
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Validates if a quantity DTO is valid
        /// </summary>
        /// <param name="quantity">The quantity to validate</param>
        /// <returns>True if quantity is valid, false otherwise</returns>
        public bool IsValid(QuantityDTO quantity)
        {
            // Check for null
            if (quantity == null)
                return false;

            // Check if unit is provided
            if (string.IsNullOrWhiteSpace(quantity.Unit))
                return false;

            // Check if measurement type is provided
            if (string.IsNullOrWhiteSpace(quantity.MeasurementType))
                return false;

            // Check if value is a finite number (not NaN or Infinity)
            if (!double.IsFinite(quantity.Value))
                return false;

            // Check if unit is valid for measurement type
            try
            {
                var measurable = CreateMeasurable(quantity.Unit, quantity.MeasurementType);
                return measurable != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if arithmetic operations can be performed on two quantities
        /// </summary>
        /// <param name="q1">First quantity</param>
        /// <param name="q2">Second quantity</param>
        /// <returns>True if arithmetic operations are possible, false otherwise</returns>
        public bool CanPerformArithmetic(QuantityDTO q1, QuantityDTO q2)
        {
            // Both quantities must be valid
            if (!IsValid(q1) || !IsValid(q2))
                return false;

            // Both quantities must be from the same measurement category
            if (q1.MeasurementType != q2.MeasurementType)
                return false;

            // Check if measurement type supports arithmetic (Temperature doesn't)
            var measurable = CreateMeasurable(q1.Unit, q1.MeasurementType);
            return measurable.SupportsArithmetic();
        }

        /// <summary>
        /// Gets detailed validation errors for a quantity
        /// </summary>
        /// <param name="quantity">The quantity to validate</param>
        /// <returns>A list of validation error messages</returns>
        public List<string> GetValidationErrors(QuantityDTO quantity)
        {
            var errors = new List<string>();

            if (quantity == null)
            {
                errors.Add("Quantity cannot be null");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(quantity.Unit))
                errors.Add("Unit cannot be empty");

            if (string.IsNullOrWhiteSpace(quantity.MeasurementType))
                errors.Add("Measurement type cannot be empty");

            if (!double.IsFinite(quantity.Value))
                errors.Add("Value must be a finite number");

            // Check if unit is valid for measurement type
            try
            {
                CreateMeasurable(quantity.Unit, quantity.MeasurementType);
            }
            catch
            {
                errors.Add($"Invalid unit '{quantity.Unit}' for measurement type '{quantity.MeasurementType}'");
            }

            return errors;
        }

        /// <summary>
        /// Private helper method to create IMeasurable helper
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