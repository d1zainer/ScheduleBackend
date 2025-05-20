using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Registration?> GetById(Guid id);
        Task<(bool success, IEnumerable<Registration>? data)> GetAll(RegistrationStatus? status);
        Task<(bool Success, Exception? Ex)> Add(Registration registration);
        Task<(bool Success, Exception? Ex, Registration? Updated)> Update(Guid id, Registration status);
        Task<(bool Success, Exception? Ex, Registration? Updated)> UpdateStatus(Guid id, RegistrationStatus status, Guid adminId);
        IQueryable<Registration> GetQueryable();
    }
}
