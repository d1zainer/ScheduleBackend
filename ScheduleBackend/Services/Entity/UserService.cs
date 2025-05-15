using Newtonsoft.Json;
using ScheduleBackend.Models;
using ScheduleBackend.Repositories.Interfaces;

namespace ScheduleBackend.Services.Entity
{
    public class UserService(IUserRepository repository)
    {
        public async Task<List<User>> GetUsers() => (await repository.GetAll()).ToList();
        public async Task<(bool success, Exception? ex)> Add(User user) => await repository.Add(user);
        public async  Task<(bool success, Exception? ex)> Delete(int id) => await repository.Delete(id);
        
        public async Task<(User? user, bool success)> Authenticate(string username, string password)
        {
            var users = await GetUsers();
            var user = users.Find(u => u.Username == username && u.Password == password);
            if (user != null) return (user, true);
            return (null, false);
        }
    }
}