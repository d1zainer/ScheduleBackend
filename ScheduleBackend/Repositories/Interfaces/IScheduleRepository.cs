using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface IScheduleRepository
    {

        Task<IEnumerable<Schedule>> GetAll();
        Task<Schedule?> GetById(Guid id);
        Task<(bool Success, Exception? Ex)> Add(Schedule schedule);
        Task<(bool Success, Exception? Ex)> Delete(Guid id);
        Task<(bool Success, Exception? Ex, Schedule? Updated)> Update(Schedule teacher);
    }
}
