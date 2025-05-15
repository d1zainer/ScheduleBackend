using ScheduleBackend.Models;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface IScheduleRepository
    {

        Task<IEnumerable<Schedule>> GetAll();
        Task<Schedule?> GetById(int id);
        Task<(bool Success, Exception? Ex)> Add(Schedule schedule);
        Task<(bool Success, Exception? Ex)> Delete(int id);
        Task<(bool Success, Exception? Ex, Schedule? Updated)> Update(Schedule teacher);
    }
}
