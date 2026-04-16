using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Model.Enums;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Mappers;
using QuantityMeasurementApp.Service.Helpers;

namespace QuantityMeasurementApp.Service.Services
{
    /// <summary>
    /// Service for arithmetic operations using YOUR ORIGINAL logic
    /// Handles addition, subtraction, and division operations on quantities
    /// </summary>
    public class ArithmeticService : IArithmeticService
    {
        // Conversion service for converting between units
        private readonly IConversionService _conversionService;

        /// <summary>
        /// Constructor that injects conversion service dependency
        /// </summary>
        /// <param name="conversionService">Service for unit conversion</param>
        public ArithmeticService(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        /// <summary>
        /// Adds two quantities and returns result in first quantity's unit
        /// </summary>
        /// <param name="q1">First quantity to add</param>
        /// <param name="q2">Second quantity to add</param>
        /// <returns>A new QuantityDTO with the sum</returns>
        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            // Convert to quantities using your helper classes
            var quantity1 = QuantityMapper.ToQuantity(q1);
            var quantity2 = QuantityMapper.ToQuantity(q2);
            
            // YOUR ORIGINAL VALIDATION LOGIC
            ValidateArithmeticOperands(quantity1, quantity2, quantity1.Unit, true);
            
            // YOUR ORIGINAL ARITHMETIC LOGIC
            double baseResult = PerformArithmetic(quantity1, quantity2, quantity1.Unit, ArithmeticOperation.ADD);
            double result = quantity1.Unit.ConvertFromBaseUnit(baseResult);
            
            return new QuantityDTO
            {
                Value = Round(result),
                Unit = q1.Unit,
                MeasurementType = q1.MeasurementType
            };
        }

        /// <summary>
        /// Adds two quantities and returns result in specified target unit
        /// </summary>
        /// <param name="q1">First quantity to add</param>
        /// <param name="q2">Second quantity to add</param>
        /// <param name="targetUnit">Target unit for the result</param>
        /// <returns>A new QuantityDTO with the sum in target unit</returns>
        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            var quantity1 = QuantityMapper.ToQuantity(q1);
            var quantity2 = QuantityMapper.ToQuantity(q2);
            var targetMeasurable = CreateMeasurable(targetUnit, q1.MeasurementType);
            
            ValidateArithmeticOperands(quantity1, quantity2, targetMeasurable, true);
            
            double baseResult = PerformArithmetic(quantity1, quantity2, targetMeasurable, ArithmeticOperation.ADD);
            double result = targetMeasurable.ConvertFromBaseUnit(baseResult);
            
            return new QuantityDTO
            {
                Value = Round(result),
                Unit = targetUnit,
                MeasurementType = q1.MeasurementType
            };
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in first quantity's unit
        /// </summary>
        /// <param name="q1">Quantity to subtract from (minuend)</param>
        /// <param name="q2">Quantity to subtract (subtrahend)</param>
        /// <returns>A new QuantityDTO with the difference</returns>
        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            var quantity1 = QuantityMapper.ToQuantity(q1);
            var quantity2 = QuantityMapper.ToQuantity(q2);
            
            ValidateArithmeticOperands(quantity1, quantity2, quantity1.Unit, true);
            
            double baseResult = PerformArithmetic(quantity1, quantity2, quantity1.Unit, ArithmeticOperation.SUBTRACT);
            double result = quantity1.Unit.ConvertFromBaseUnit(baseResult);
            
            return new QuantityDTO
            {
                Value = Round(result),
                Unit = q1.Unit,
                MeasurementType = q1.MeasurementType
            };
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in specified target unit
        /// </summary>
        /// <param name="q1">Quantity to subtract from (minuend)</param>
        /// <param name="q2">Quantity to subtract (subtrahend)</param>
        /// <param name="targetUnit">Target unit for the result</param>
        /// <returns>A new QuantityDTO with the difference in target unit</returns>
        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            var quantity1 = QuantityMapper.ToQuantity(q1);
            var quantity2 = QuantityMapper.ToQuantity(q2);
            var targetMeasurable = CreateMeasurable(targetUnit, q1.MeasurementType);
            
            ValidateArithmeticOperands(quantity1, quantity2, targetMeasurable, true);
            
            double baseResult = PerformArithmetic(quantity1, quantity2, targetMeasurable, ArithmeticOperation.SUBTRACT);
            double result = targetMeasurable.ConvertFromBaseUnit(baseResult);
            
            return new QuantityDTO
            {
                Value = Round(result),
                Unit = targetUnit,
                MeasurementType = q1.MeasurementType
            };
        }

        /// <summary>
        /// Divides first quantity by second quantity
        /// </summary>
        /// <param name="q1">Quantity to divide (dividend)</param>
        /// <param name="q2">Quantity to divide by (divisor)</param>
        /// <returns>The ratio of the two quantities as a double</returns>
        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            var quantity1 = QuantityMapper.ToQuantity(q1);
            var quantity2 = QuantityMapper.ToQuantity(q2);
            
            ValidateArithmeticOperands(quantity1, quantity2, default, false);
            
            return PerformArithmetic(quantity1, quantity2, default, ArithmeticOperation.DIVIDE);
        }

        /// <summary>
        /// YOUR ORIGINAL CENTRALIZED ARITHMETIC METHOD
        /// Performs the actual arithmetic operation on base unit values
        /// </summary>
        /// <param name="q1">First quantity</param>
        /// <param name="q2">Second quantity</param>
        /// <param name="targetUnit">Target unit (not used for division)</param>
        /// <param name="operation">The arithmetic operation to perform</param>
        /// <returns>Result of arithmetic operation in base units</returns>
        private double PerformArithmetic(
            Quantity<IMeasurable> q1, 
            Quantity<IMeasurable> q2, 
            IMeasurable targetUnit, 
            ArithmeticOperation operation)
        {
            // YOUR ORIGINAL ValidateOperationSupport
            q1.Unit.ValidateOperationSupport(operation.ToString());
            q2.Unit.ValidateOperationSupport(operation.ToString());

            // Convert both quantities to base units
            double base1 = q1.Unit.ConvertToBaseUnit(q1.Value);
            double base2 = q2.Unit.ConvertToBaseUnit(q2.Value);

            // Perform the requested operation
            switch (operation)
            {
                case ArithmeticOperation.ADD:
                    return base1 + base2;

                case ArithmeticOperation.SUBTRACT:
                    return base1 - base2;

                case ArithmeticOperation.DIVIDE:
                    if (base2 == 0)
                        throw new ArithmeticException("Division by zero");
                    return base1 / base2;

                default:
                    throw new InvalidOperationException("Unsupported operation");
            }
        }

        /// <summary>
        /// YOUR ORIGINAL CENTRAL VALIDATION METHOD
        /// Validates operands before performing arithmetic operations
        /// </summary>
        /// <param name="q1">First quantity</param>
        /// <param name="q2">Second quantity</param>
        /// <param name="targetUnit">Target unit (if required)</param>
        /// <param name="targetUnitRequired">Whether target unit is required for the operation</param>
        private void ValidateArithmeticOperands(
            Quantity<IMeasurable> q1, 
            Quantity<IMeasurable> q2, 
            IMeasurable targetUnit, 
            bool targetUnitRequired)
        {
            if (q2 == null)
                throw new ArgumentException("Operand quantity cannot be null");

            // Check if both quantities are from the same measurement category
            if (!q1.Unit.GetType().Equals(q2.Unit.GetType()))
                throw new ArgumentException("Incompatible measurement categories");

            // Validate numeric values
            if (!double.IsFinite(q1.Value) || !double.IsFinite(q2.Value))
                throw new ArgumentException("Invalid numeric values");

            // Validate target unit if required
            if (targetUnitRequired && targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");
        }

        /// <summary>
        /// Rounds a value to 2 decimal places
        /// </summary>
        /// <param name="val">Value to round</param>
        /// <returns>Rounded value</returns>
        private double Round(double val)
        {
            return Math.Round(val, 2);
        }

        /// <summary>
        /// Private helper method to create IMeasurable helper
        /// </summary>
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