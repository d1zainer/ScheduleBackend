using Newtonsoft.Json;
using ScheduleBackend.Models;

namespace ScheduleBackend.Services
{
    public class TeachersService
    {
        private string _filePath => JsonService.Teachers;

        public List<Teacher> LoadAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Teacher>();
            }
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Teacher>>(json);
        }

        public void Save(List<Teacher> schedules)
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
        public bool Add(Teacher user)
        {
            var users = LoadAll();
            try
            {
                users.Add(user);
                Save(users);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            var users = LoadAll();
            var user = users.Where(u => u.Id == id).First();
            try
            {
                users.Remove(user);
                Save(users);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Teacher> GetUsers()
        {
            return LoadAll(); ;
        }

        public int? GetActiveSlots(int id)
        {
            var teachers = LoadAll();
            var t = teachers.Find(x => x.Id == id);
            if(t != null)
                return t.ActiveSlots;
            else
                return null;
        }


        public TeacherUpdateResponse? UpdateActiveSlots(TeacherUpdateRequest teacher)
        {
            var teachers = LoadAll();
            var t = teachers.Find(x => x.Id == teacher.Id);
            if (t.ActiveSlots == 0)
                return  new TeacherUpdateResponse(t, "Количество слотов не обновлено", false);

            if (teacher.Action == 0)
            {
                t.ActiveSlots++;
                return new (t, "Количесвто слотов увеличилось", true);
            }
            else
            {
                t.ActiveSlots--;
                return new(t, "Количесвто слотов уменьшилось", true);
            }
        }
        public List<Teacher>? GetTeachersByGroupId(int id)
        {
            var teachers = LoadAll();
            var t = teachers.Where(x => x.GroupId == id).ToList();
            if (t.Count == 0)
            {
                return null;
            }

            return t;
        }
    }
}
