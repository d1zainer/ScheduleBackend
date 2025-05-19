using Microsoft.EntityFrameworkCore;
using ScheduleBackend.Db;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;

namespace ScheduleBackend.Repositories
{
    public class AdminRepository(ScheduleDbContext context) : IAdminRepository
    {
        public async Task<(bool Success, Exception? Ex)> Add(Admin admin)
        {
            try
            {
                await context.Admins.AddAsync(admin);
                await context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public async Task<(bool Success, Exception? Ex)> Delete(Guid id)
        {
            try
            {
                var admin = await context.Admins.FindAsync(id);
                if (admin == null)
                    return (false, null);

                context.Admins.Remove(admin);
                await context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public async Task<IEnumerable<Admin>> GetAll()
        {
            return await context.Admins.ToListAsync();
        }

        public async Task<Admin?> GetById(Guid id)
        {
            return await context.Admins.FindAsync(id);
        }

        public IQueryable<Admin> GetQueryable()
        {
            return context.Admins;
        }

        public async Task<(bool Success, Exception? Ex, Admin? Updated)> Update(Admin admin)
        {
            try
            {
                var existing = await context.Admins.FindAsync(admin.Id);
                if (existing == null)
                    return (false, null, null);

                existing.FirstName = admin.FirstName;
                existing.LastName = admin.LastName;
                existing.MiddleName = admin.MiddleName;
                existing.Login = admin.Login;
                existing.Password = admin.Password;
                existing.Email = admin.Email;
                existing.PhoneNumber = admin.PhoneNumber;

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
