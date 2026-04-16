using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Helpers;
using QuantityMeasurementApp.Model.Enums;

namespace QuantityMeasurementApp.Service.Mappers
{
    /// <summary>
    /// Mapper for converting between DTOs and Quantity entities
    /// Provides static methods to transform data objects across different layers
    /// </summary>
    public static class QuantityMapper
    {
        /// <summary>
        /// Converts a QuantityDTO to a Quantity entity with IMeasurable unit
        /// </summary>
        /// <param name="dto">The data transfer object containing value, unit, and measurement type</param>
        public static Quantity<IMeasurable> ToQuantity(QuantityDTO dto)
        {
            // Create the appropriate measurable helper based on unit and measurement type
            var measurable = CreateMeasurable(dto.Unit, dto.MeasurementType);

            // Return new Quantity entity with the value and measurable unit
            return new Quantity<IMeasurable>(dto.Value, measurable);
        }

        /// <summary>
        /// Converts a Quantity entity to a QuantityDTO
        /// </summary>
        /// <returns>A QuantityDTO with value, unit name, and measurement type</returns>
        public static QuantityDTO ToDTO<T>(Quantity<T> quantity) where T : IMeasurable
        {
            return new QuantityDTO
            {
                Value = quantity.Value,
                Unit = quantity.Unit.GetUnitName(), // Get unit name from IMeasurable
                MeasurementType = quantity.Unit.GetMeasurementType() // Get measurement type from IMeasurable
            };
        }

        /// <summary>
        /// Converts a Quantity entity to a QuantityModel for internal service use
        /// </summary>
        /// <returns>A QuantityModel with value and unit</returns>
        public static QuantityModel<T> ToModel<T>(Quantity<T> quantity) where T : IMeasurable
        {
            return new QuantityModel<T>(quantity.Value, quantity.Unit);
        }

        /// <summary>
        /// Converts a QuantityModel back to a Quantity entity
        /// </summary>
        /// <returns>A Quantity entity with value and unit from the model</returns>
        public static Quantity<T> FromModel<T>(QuantityModel<T> model) where T : IMeasurable
        {
            return new Quantity<T>(model.Value, model.Unit);
        }

        /// <summary>
        /// Private helper method to create the appropriate IMeasurable helper based on unit name and measurement type
        /// </summary>
        private static IMeasurable CreateMeasurable(string unitName, string measurementType)
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