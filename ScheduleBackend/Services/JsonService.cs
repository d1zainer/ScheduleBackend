using Newtonsoft.Json;

namespace ScheduleBackend.Services
{
    public class JsonService
    {
        public static string Users => Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Users.json");
        public static string Teachers => Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Teachers.json");
        public static string UsersSchedules => Path.Combine(Directory.GetCurrentDirectory(), "Storage", "UsersSchedules.json");
        public static string TeachersSchedules => Path.Combine(Directory.GetCurrentDirectory(), "Storage", "TeachersSchedules.json");

        public static List<T> GetJson<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }

                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (IOException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Save<T>(List<T> list, string path)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(list);
                File.WriteAllText(path, jsonData);
            }
            catch (IOException exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}