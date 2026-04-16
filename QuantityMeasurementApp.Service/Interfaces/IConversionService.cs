using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Service.Interfaces
{
    public interface IConversionService
    {
        QuantityDTO Convert(QuantityDTO source, string targetUnit);
        double ToBaseUnit(QuantityDTO quantity);
        QuantityDTO FromBaseUnit(double baseValue, string targetUnit, string measurementType);
    }
}