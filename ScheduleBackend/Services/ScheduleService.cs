using Newtonsoft.Json;
using ScheduleBackend.Models;

namespace ScheduleBackend.Services
{
    public class ScheduleService
    {
        private string _jsonFilePath => JsonService.UsersSchedules;

        

        public List<Schedule> GetSchedules()
        {
            try
            {
                var jsonData = File.ReadAllText(_jsonFilePath);
                var userSchedules = JsonConvert.DeserializeObject<RootObject>(jsonData);
                return userSchedules.UsersSchedules ?? new List<Schedule>();
            }
            catch (IOException)
            {
                return new List<Schedule>();
            }
            catch (JsonException)
            {
                return new List<Schedule>();
            }
        }

        public Schedule GetScheduleByUserId(int userId)
        {
            var schedules = GetSchedules();
            return schedules.FirstOrDefault(u => u.ScheduleId == userId);
        }
        public Schedule? UpdateSchedule(ScheduleUpdateRequest updateRequest)
        {
            var schedules = GetSchedules();
            var schedule = schedules.FirstOrDefault(s => s.ScheduleId == updateRequest.ScheduleId);
            if (schedule == null)
            {
                return null; 
            }
            var daySchedule = schedule.Days.FirstOrDefault(day => day.DayNumber == updateRequest.DayNumber);
            if (daySchedule == null)
            {
                return null; 
            }
            var activity = daySchedule.Activities.FirstOrDefault(c => c.ActivityNumber == updateRequest.ActivityNumber);
            if (activity == null)
            {
                return null; 
            }
            if (updateRequest.NewLessonName == null)
            {
                activity.LessonName = null; // Очистка названия курса
            }
            activity.IsBooked = updateRequest.IsBooked;
            activity.LessonName = updateRequest.NewLessonName;
            SaveSchedules(schedules);
            return schedule;
        }
        public (bool, List<ScheduleCheckRequest>?) CheckActivities(List<ScheduleCheckRequest> updateRequest)
        {
            bool result = true; 
            List<ScheduleCheckRequest>? bookedActivities = null;
            var schedules = GetSchedules();
            foreach (var sh in updateRequest)
            {
                var schedule = schedules.FirstOrDefault(s => s.ScheduleId == sh.ScheduleId);
                if (schedule == null) continue;
                var daySchedule = schedule.Days.FirstOrDefault(day => day.DayNumber == sh.DayNumber);
                if (daySchedule == null) continue;
                var activity = daySchedule.Activities.FirstOrDefault(c => c.ActivityNumber == sh.ActivityNumber);
                if (activity == null || !activity.IsBooked) continue;
                result = false;
                if (bookedActivities == null) bookedActivities = new List<ScheduleCheckRequest>();
                bookedActivities.Add(sh);
            }
            return (result, bookedActivities);
        }
        public bool CheckActivity(ScheduleCheckRequest updateRequest)
        {
            bool result = false;
            var schedules = GetSchedules();
            var schedule = schedules.FirstOrDefault(s => s.ScheduleId == updateRequest.ScheduleId);
            var daySchedule = schedule.Days.FirstOrDefault(day => day.DayNumber == updateRequest.DayNumber);
            var activity = daySchedule.Activities.FirstOrDefault(c => c.ActivityNumber == updateRequest.ActivityNumber);
            if (activity == null || !activity.IsBooked)
                result = true;
            return result;
        }
        private void SaveSchedules(List<Schedule> schedules)
        {
            try
            {
                var rootObject = new RootObject
                {
                    UsersSchedules = schedules
                };
                var jsonData = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
                File.WriteAllText(_jsonFilePath, jsonData);
            }
            catch (IOException)
            {
                // Обработка ошибок при записи в файл
            }
        }
    }
}
