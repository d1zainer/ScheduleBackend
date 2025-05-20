using Microsoft.EntityFrameworkCore;
using ScheduleBackend.Db;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;

namespace ScheduleBackend.Repositories.Db
{
    public class RegistrationRepository(ScheduleDbContext context) : IRegistrationRepository
    {
        public async Task<(bool Success, Exception? Ex)> Add(Registration registration)
        {
            try
            {
                await context.AddAsync(registration);
                await context.SaveChangesAsync();
                return (true, null);
            }catch(Exception ex) {return (false, ex); }
        }

        public async Task<(bool success, IEnumerable<Registration>? data)> GetAll(RegistrationStatus? status)
        {
            try
            {
                IQueryable<Registration> query = context.Registrations;
                if (status.HasValue) query = query.Where(r => r.Status == status.Value);
                var registrations = await query.ToListAsync();
                return (true, registrations);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }


        public Task<Registration?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        

        public IQueryable<Registration> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, Exception? Ex, Registration? Updated)> Update(Guid id, Registration status)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool Success, Exception? Ex, Registration? Updated)> UpdateStatus(Guid id, RegistrationStatus status, Guid adminId)
        {
            try
            {
                var existing = await context.Registrations.FirstOrDefaultAsync(r => r.Id == id);
                if (existing == null)
                    return (false, null, null);

                existing.Status = status;
                existing.AdminId = adminId;

                await context.SaveChangesAsync();
                return (true, null, existing);
            }
            catch (Exception ex)
            {
                return (false, ex, null);
            }
        }
    }
}
