using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuantityMeasurementApp.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public string Register(string email, string password)
        {
            try
            {
                string hash = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new User
                {
                    Email = email,
                    PasswordHash = hash
                };

                _repo.Register(user);

                // THIS LINE IS FAILING
                return GenerateToken(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                throw;
            }
        }

        public string Login(string email, string password)
        {
            var user = _repo.GetUserByEmail(email);

            if (user == null)
                return null;

            // Verify password
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValid)
                return null;

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,   user.Id.ToString()),
                new Claim(ClaimTypes.Email,              user.Email),       
                new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}