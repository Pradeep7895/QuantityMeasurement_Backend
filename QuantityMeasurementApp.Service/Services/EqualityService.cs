using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Mappers;

namespace QuantityMeasurementApp.Service.Services
{
    /// <summary>
    /// Service for equality operations using YOUR ORIGINAL logic
    /// UC-1 to UC-9
    /// Handles comparison of quantities for equality
    /// </summary>
    public class EqualityService : IEqualityService
    {
        // Tolerance for floating point comparison
        private const double EPSILON = 0.0001;

        /// <summary>
        /// Compares two quantities for equality
        /// </summary>
        /// <param name="q1">First quantity to compare</param>
        /// <param name="q2">Second quantity to compare</param>
        /// <returns>True if quantities are equal (within tolerance), false otherwise</returns>
        public bool AreEqual(QuantityDTO q1, QuantityDTO q2)
        {
            // First check if they are from the same measurement category
            if (!AreSameCategory(q1, q2))
                return false;

            // Convert DTOs to Quantity entities
            var quantity1 = QuantityMapper.ToQuantity(q1);
            var quantity2 = QuantityMapper.ToQuantity(q2);

            // YOUR ORIGINAL EQUALS LOGIC
            // Convert both to base units for comparison
            double base1 = quantity1.Unit.ConvertToBaseUnit(quantity1.Value);
            double base2 = quantity2.Unit.ConvertToBaseUnit(quantity2.Value);

            // Compare with tolerance to handle floating point precision issues
            return Math.Abs(base1 - base2) < EPSILON;
        }

        /// <summary>
        /// Checks if two quantities are from the same measurement category
        /// </summary>
        /// <param name="q1">First quantity</param>
        /// <param name="q2">Second quantity</param>
        /// <returns>True if both quantities have the same measurement type</returns>
        public bool AreSameCategory(QuantityDTO q1, QuantityDTO q2)
        {
            return q1.MeasurementType == q2.MeasurementType;
        }
    }
}