using Microsoft.AspNetCore.Mvc;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Services.Auth;

namespace ScheduleBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.Authenticate(request);
            if (!token.success)
                return Unauthorized(new { message = "Неверный логин или пароль" });
            return Ok(token.response);
        }
    }
}
