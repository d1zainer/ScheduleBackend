using ScheduleBackend.Models;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAll();
        Task<Teacher?> GetById(int id);
        Task<IEnumerable<Teacher>> GetByGroupId(int groupId);
        Task<(bool Success, Exception? Ex)> Add(Teacher teacher);
        Task<(bool Success, Exception? Ex)> Delete(int id);
        Task<(bool Success, Exception? Ex, Teacher? Updated)> Update(Teacher teacher);
    }
}
