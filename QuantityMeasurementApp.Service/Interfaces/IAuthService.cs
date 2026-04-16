namespace QuantityMeasurementApp.Service.Interfaces
{
    public interface IAuthService
    {
        string Register(string email, string password);
        string Login(string email, string password);
    }
}