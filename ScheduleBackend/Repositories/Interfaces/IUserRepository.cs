using ScheduleBackend.Models;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<(bool success, Exception? error)> Add(User user);
        Task<(bool success, Exception? error)> Delete(int id);
    }
}
