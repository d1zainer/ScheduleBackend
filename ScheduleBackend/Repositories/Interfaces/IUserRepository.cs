using Microsoft.AspNetCore.Identity;
using ScheduleBackend.Models.Dto;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<LoginUserInfo?> GetUserByLoginAsync(string login);
    }
}
