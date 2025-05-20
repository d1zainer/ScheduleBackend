using Microsoft.EntityFrameworkCore;
using ScheduleBackend.Db;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services.Interfaces;

namespace ScheduleBackend.Services.Auth
{
    public class AuthService
    {

        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        private readonly IUserRepository _userRepository;
        public AuthService(IJwtService jwtService, ILogger<AuthService> logger, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<(bool success, LoginResponse? response)> Authenticate(LoginRequest request)
        {

            var login = request.Login;
            var password = request.Password;
            var user = await _userRepository.GetUserByLoginAsync(login);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                var token = _jwtService.GenerateToken(user.Id, user.Role.ToString().ToLower());
                return (true, new LoginResponse
                {
                    Token = token.token,
                    ExpiresIn = token.expiresInDays
                });
            }

            return (false, null);
        }

    }
}
