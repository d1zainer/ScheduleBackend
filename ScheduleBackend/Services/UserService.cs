using Newtonsoft.Json;
using ScheduleBackend.Models;

namespace ScheduleBackend.Services
{
    public class UserService
    {
        private string _filePath => JsonService.Users;

        public List<User> LoadUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public void SaveUsers(List<User> schedules)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(schedules);
                File.WriteAllText(_filePath, jsonData);
            }
            catch (IOException)
            {
            }
        }

        public bool Add(User user)
        {
            var users = LoadUsers();
            try
            {
                users.Add(user);

                SaveUsers(users);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var users = LoadUsers();
            var user = users.Where(u => u.Id == id).First();
            try
            {
                users.Remove(user);
                SaveUsers(users);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetUsers()
        {
            return LoadUsers(); ;
        }

        public (User?, bool) Authenticate(string username, string password)
        {
            var users = LoadUsers();
            var user = users.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                return (user, true);
            }
            return (null, false);
        }
    }
}