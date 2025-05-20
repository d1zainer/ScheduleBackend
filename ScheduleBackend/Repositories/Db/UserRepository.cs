using Microsoft.EntityFrameworkCore;
using ScheduleBackend.Db;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;

namespace ScheduleBackend.Repositories.Db
{
    public class UserRepository(ScheduleDbContext context) : IUserRepository
    {
        public async Task<LoginUserInfo?> GetUserByLoginAsync(string login)
        {
            var users = context.Students
                .Select(u => new { u.Id, u.Login, u.Password, Role = UserRole.User });

            var teachers = context.Teachers
                .Select(t => new { t.Id, t.Login, t.Password, Role = UserRole.Teacher });

            var admins = context.Admins
                .Select(a => new { a.Id, a.Login, a.Password, Role = UserRole.Admin });

            var allUsers = users
                .Union(teachers)
                .Union(admins);
            var user = await allUsers
                .Where(u => u.Login == login)
                .FirstOrDefaultAsync();

            return user is null
                ? null
                : new LoginUserInfo(user.Id, user.Login, user.Password, user.Role);
        }
    }
}
