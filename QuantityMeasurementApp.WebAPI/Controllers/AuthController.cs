using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(string email, string password)
        {
            var token = _service.Register(email, password);
            return Ok(token);
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var token = _service.Login(email, password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}