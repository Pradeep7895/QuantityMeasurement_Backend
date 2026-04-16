using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Service.Interfaces
{
    public interface IEqualityService
    {
        bool AreEqual(QuantityDTO q1, QuantityDTO q2);
        bool AreSameCategory(QuantityDTO q1, QuantityDTO q2);
    }
}