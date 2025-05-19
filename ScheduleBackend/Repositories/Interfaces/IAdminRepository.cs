using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAll();
        Task<Admin?> GetById(Guid id);
        Task<(bool Success, Exception? Ex)> Add(Admin schedule);
        Task<(bool Success, Exception? Ex)> Delete(Guid id);
        Task<(bool Success, Exception? Ex, Admin? Updated)> Update(Admin teacher);
        IQueryable<Admin> GetQueryable();
    }
}