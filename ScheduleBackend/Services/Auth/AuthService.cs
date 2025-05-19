using Microsoft.EntityFrameworkCore;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services.Interfaces;

namespace ScheduleBackend.Services.Auth
{
    public class AuthService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IStudentRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        public AuthService(ITeacherRepository teacherRepository, IAdminRepository adminRepository,
            IStudentRepository userRepository, IJwtService jwtService, ILogger<AuthService> logger)
        {
            _teacherRepository = teacherRepository;
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<(bool success, LoginResponse? response)> Authenticate(LoginRequest request)
        {

            var login = request.Login;
            var password = request.Password;

            var usersQuery = _userRepository.GetQueryable()
                .Select(u => new
                {
                    u.Id,
                    u.Login,
                    u.Password,
                    Role = UserRole.User
                });

            var teachersQuery = _teacherRepository.GetQueryable()
                .Select(t => new
                {
                    t.Id,
                    t.Login,
                    t.Password,
                    Role = UserRole.Teacher
                });

            var adminsQuery = _adminRepository.GetQueryable()
                .Select(a => new
                {
                    a.Id,
                    a.Login,
                    a.Password,
                    Role = UserRole.Admin
                });

            var allUsers = usersQuery
                .Union(teachersQuery)
                .Union(adminsQuery);

            var user = await allUsers
                .Where(u => u.Login == login)
                .FirstOrDefaultAsync();

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                var token = _jwtService.GenerateToken(user.Id, user.Role.ToString().ToLower());
                return (true, new LoginResponse
                {
                    Token = token.token,
                    ExpiresIn = token.expiresInDays
                });
            }

            // Никого не нашли или пароль не подошёл
            return (false, null);
        }

    }
}
