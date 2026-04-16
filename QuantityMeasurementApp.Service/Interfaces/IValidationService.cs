using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Service.Interfaces
{
    public interface IValidationService
    {
        bool IsValid(QuantityDTO quantity);
        bool CanPerformArithmetic(QuantityDTO q1, QuantityDTO q2);
        List<string> GetValidationErrors(QuantityDTO quantity);
    }
}