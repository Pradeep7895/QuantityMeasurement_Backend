using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void Register(User user);
    }
}