using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAll();
        Task<Teacher?> GetById(Guid id);
        Task<IEnumerable<Teacher>> GetByGroupId(int groupId);
        Task<(bool Success, Exception? Ex)> Add(Teacher teacher);
        Task<(bool Success, Exception? Ex)> Delete(Guid id);
        Task<(bool Success, Exception? Ex, Teacher? Updated)> Update(Teacher teacher);
        IQueryable<Teacher> GetQueryable();
    }
}
