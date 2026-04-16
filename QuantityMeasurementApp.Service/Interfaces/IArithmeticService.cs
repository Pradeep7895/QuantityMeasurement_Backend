using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Enums;

namespace QuantityMeasurementApp.Service.Interfaces
{
    public interface IArithmeticService
    {
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit);
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit);
        double Divide(QuantityDTO q1, QuantityDTO q2);
    }
}