using Microsoft.EntityFrameworkCore;
using ScheduleBackend.Db;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services;

namespace ScheduleBackend.Repositories.Db
{
    public class UserRepository(ScheduleDbContext context) : IUserRepository
    {

        public async Task<(bool success, Exception? error)> Add(Student user)
        {

            try
            {
                await context.Students.AddAsync(user);
                await context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex) { return (false, ex); }
        }


        public async Task<(bool success, Exception? error)> Delete(Guid id) 
        {
            try
            {

                return (true, null);
            }
            catch (Exception ex) { return (false, ex); }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await context.Students.ToListAsync();
        }

        public IQueryable<Student> GetQueryable()
        {
             return context.Students;
        }
    }
}
