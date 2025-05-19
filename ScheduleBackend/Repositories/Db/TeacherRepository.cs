using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScheduleBackend.Db;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services;

namespace ScheduleBackend.Repositories.Db
{
    public class TeacherRepository(ScheduleDbContext context) : ITeacherRepository
    {

        public async Task<IEnumerable<Teacher>> GetAll() => await context.Teachers.ToListAsync();
      

        public async Task<Teacher?> GetById(Guid id)
        {
            var teachers = await GetAll();
            return teachers.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Teacher>> GetByGroupId(int groupId)
        {
            var teachers = await GetAll();
            return teachers.Where(x => x.GroupId == groupId);
        }

        public async Task<(bool Success, Exception? Ex)> Add(Teacher teacher)
        {
            try
            {
                await context.Teachers.AddAsync(teacher);
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
                var teacher = await context.Teachers.FindAsync(id);
                if (teacher == null)
                    return (false, null);

                context.Teachers.Remove(teacher);
                await context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public async Task<(bool Success, Exception? Ex, Teacher? Updated)> Update(Teacher teacher)
        {
            try
            {
                var existing = await context.Teachers.FindAsync(teacher.Id);
                if (existing == null)
                    return (false, null, null);


                existing.FirstName = teacher.FirstName;
                existing.LastName = teacher.LastName;
                existing.MiddleName = teacher.MiddleName;
                existing.Login = teacher.Login;
                existing.Password = teacher.Password;
                existing.Email = teacher.Email;

                await context.SaveChangesAsync();
                return (true, null, teacher);
            }
            catch (Exception ex)
            {
                return (false, ex, null);
            }
        }

        public IQueryable<Teacher> GetQueryable()
        {
            return context.Teachers;
        }
    }
}
