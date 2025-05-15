using ScheduleBackend.Models;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services;

namespace ScheduleBackend.Repositories.Json
{
    public class JsonUserRepository : IUserRepository
    {
        private readonly string _jsonFilePath = JsonService.Users;
        private async Task SaveAll(List<User> teachers) => await JsonService.Save(teachers, _jsonFilePath);
        private async Task<List<User>> LoadAll() => await JsonService.GetJson<User>(_jsonFilePath);

        public async Task<(bool success, Exception? error)> Add(User user)
        {
            try
            {
                var teachers = await LoadAll();
                teachers.Add(user);
                await SaveAll(teachers);
                return (true, null);
            }
            catch (Exception ex) { return (false, ex); }
        }


        public async Task<(bool success, Exception? error)> Delete(int id)
        {
            try
            {
                var teachers = await LoadAll();
                var teacher = teachers.FirstOrDefault(x => x.Id == id);
                if (teacher == null)
                    return (false, null);

                teachers.Remove(teacher);
                await SaveAll(teachers);
                return (true, null);
            }
            catch (Exception ex) { return (false, ex); }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var result = await JsonService.GetJson<User>(_jsonFilePath);
            return result;
        }
    }
}
