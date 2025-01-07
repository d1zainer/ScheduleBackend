using Newtonsoft.Json;
using ScheduleBackend.Models;

namespace ScheduleBackend.Services
{
    public class LessonService
    {
        private string _jsonFilePath => JsonService.TeachersSchedules;

        public List<TeacherSchedule?> LoadTeachersSchedules()
        {
            if (!File.Exists(_jsonFilePath))
            {
                return new List<TeacherSchedule>();
            }
            var json = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<List<TeacherSchedule>>(json);
        }

        private void SaveTeachersSchedules(List<TeacherSchedule> schedules)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(schedules);
                File.WriteAllText(_jsonFilePath, jsonData);
            }
            catch (IOException)
            {
            }
        }

        public List<TeacherSchedule> GetTeachersSchedules()
        {
            return LoadTeachersSchedules();
        }

        public List<Lesson?> GetLessons(int teacherId)
        {
            var teacher = GetTeachersSchedules().Find(x => x.TeacherId == teacherId);
            if (teacher == null)
            {
                return null;
            }
            return teacher.Lessons;
        }
    }
}