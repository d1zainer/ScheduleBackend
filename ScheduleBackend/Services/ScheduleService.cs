using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
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

        public void Add(int id)
        {
            var schedules = GetSchedules();

            // Проверяем, существует ли расписание с таким ID
            bool exists = schedules.Any(x => x.ScheduleId == id);
            if (!exists)
            {
         
                var newSchedule = new Schedule(id, GenerateDefaultDays());

                schedules.Add(newSchedule);
                SaveSchedules(schedules);

                Console.WriteLine($"Новое расписание с ID {id} создано и добавлено.");
            }
            else
            {
                Console.WriteLine($"Расписание с ID {id} уже существует.");
            }
        }

        private List<DaySchedule> GenerateDefaultDays()
        {
            var defaultDays = new List<DaySchedule>();

            for (int dayNumber = 1; dayNumber <= 6; dayNumber++) // Три дня в расписании
            {
                var activities = new List<Activity>();

              
                for (int activityNumber = 1; activityNumber <= 6; activityNumber++)
                {
                    activities.Add(new Activity
                    {
                        ActivityNumber = activityNumber,
                        StartTime = GetStartTime(activityNumber),
                        EndTime = GetEndTime(activityNumber),
                        IsBooked = false,
                        LessonName = null
                    });
                }

                defaultDays.Add(new DaySchedule
                {
                    DayNumber = dayNumber,
                    Activities = activities
                });
            }

            return defaultDays;
        }

        // Получение времени начала занятия
        private string GetStartTime(int activityNumber)
        {
            var baseHour = 8 + (activityNumber - 1) * 2; // Начало занятий с 8:00 с интервалом 2 часа
            return $"{baseHour:D2}:00";
        }

        // Получение времени окончания занятия
        private string GetEndTime(int activityNumber)
        {
            var baseHour = 8 + (activityNumber - 1) * 2 + 1; // Окончание занятий через 1.5 часа
            return $"{baseHour:D2}:30";
        }

       

    }
}
