using Newtonsoft.Json;

namespace ScheduleBackend.Services
{
    public class JsonService
    {
        public static string Users => Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Users.json");
        public static string Teachers => Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Teachers.json");
        public static string UsersSchedules => Path.Combine(Directory.GetCurrentDirectory(), "Storage", "UsersSchedules.json");
        public static string TeachersSchedules => Path.Combine(Directory.GetCurrentDirectory(), "Storage", "TeachersSchedules.json");

        public static async Task<List<T>> GetJson<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException();
                var json = await File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (IOException exception) {  throw new Exception(exception.Message);}
        }

        public static async Task Save(IEnumerable<object> list, string path)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(list, Formatting.Indented);
                await File.WriteAllTextAsync(path, jsonData);
            }
            catch (IOException exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}