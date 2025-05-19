using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();
        Task<(bool success, Exception? error)> Add(Student user);
        Task<(bool success, Exception? error)> Delete(Guid id);
        IQueryable<Student> GetQueryable();
    }
}
