using Newtonsoft.Json;
using ScheduleBackend.Models;
using ScheduleBackend.Repositories.Interfaces;

namespace ScheduleBackend.Services.Entity
{
    public class ScheduleService(IScheduleRepository repository)
    {

        public async Task<List<Schedule>> GetSchedules() => (await repository.GetAll()).ToList();

        public async Task<Schedule?> GetScheduleByUserId(int userId) => await repository.GetById(userId);

        public async Task<Schedule?> UpdateSchedule(ScheduleUpdateRequest updateRequest)
        {
            var schedules = await GetSchedules();
            var schedule = schedules.FirstOrDefault(s => s.ScheduleId == updateRequest.ScheduleId);
            if (schedule == null) return null;

            var daySchedule = schedule.Days.FirstOrDefault(day => day.DayNumber == updateRequest.DayNumber);
            if (daySchedule == null) return null;
           
            var activity = daySchedule.Activities.FirstOrDefault(c => c.ActivityNumber == updateRequest.ActivityNumber);
            if (activity == null) return null;
            
            if (updateRequest.NewLessonName == null)
                activity.LessonName = null; 

            activity.IsBooked = updateRequest.IsBooked;
            activity.LessonName = updateRequest.NewLessonName; 

            var (success, ex, scheduleUpdated) = await repository.Update(schedule);
            return success ? scheduleUpdated : null;
        }

        public async Task<(bool, List<ActivityCheck>?)> CheckActivities(List<ActivityCheck> updateRequest)
        {
            bool result = true;
            List<ActivityCheck>? bookedActivities = null;
            var schedules = await GetSchedules();
            foreach (var sh in updateRequest)
            {
                var schedule = schedules.FirstOrDefault(s => s.ScheduleId == sh.ScheduleId);
                if (schedule == null) continue;
                var daySchedule = schedule.Days.FirstOrDefault(day => day.DayNumber == sh.DayNumber);
                if (daySchedule == null) continue;
                var activity = daySchedule.Activities.FirstOrDefault(c => c.ActivityNumber == sh.ActivityNumber);
                if (activity == null || !activity.IsBooked) continue;
                result = false;
                if (bookedActivities == null) bookedActivities = new List<ActivityCheck>();
                bookedActivities.Add(new ActivityCheck()
                {
                    ScheduleId = sh.ScheduleId,
                    ActivityNumber = sh.ActivityNumber,
                    DayNumber = sh.DayNumber,
                    LessonName = activity.LessonName,
                });
            }
            return (result, bookedActivities);
        }

        public async Task<CourseCheckResponse> CheckCourse(CourseCheckRequest updateRequest)
        {
            bool result = true;
            List<ActivityCheck>? bookedActivities = null;
            var schedules = await GetSchedules();
            foreach (var sh in updateRequest.Activities)
            {
                var schedule = schedules.FirstOrDefault(s => s.ScheduleId == sh.ScheduleId);
                if (schedule == null) continue;
                var daySchedule = schedule.Days.FirstOrDefault(day => day.DayNumber == sh.DayNumber);
                if (daySchedule == null) continue;
                var activity = daySchedule.Activities.FirstOrDefault(c => c.ActivityNumber == sh.ActivityNumber);
                if (activity == null || activity.IsBooked && activity.LessonName == updateRequest.LessonName) continue;
                result = false;
                if (bookedActivities == null) bookedActivities = new List<ActivityCheck>();
                bookedActivities.Add(new ActivityCheck()
                {
                    ScheduleId = sh.ScheduleId,
                    ActivityNumber = sh.ActivityNumber,
                    DayNumber = sh.DayNumber,
                    LessonName = activity.LessonName,
                });
            }

            return new CourseCheckResponse
            {
                Result = result,
                BookedActivities = bookedActivities
            };
        }

        public async Task<bool> CheckActivity(ActivityCheck updateRequest)
        {
            bool result = false;
            var schedules = await GetSchedules();
            var schedule = schedules.FirstOrDefault(s => s.ScheduleId == updateRequest.ScheduleId);
            var daySchedule = schedule.Days.FirstOrDefault(day => day.DayNumber == updateRequest.DayNumber);
            var activity = daySchedule.Activities.FirstOrDefault(c => c.ActivityNumber == updateRequest.ActivityNumber);
            if (activity == null || !activity.IsBooked)
                result = true;
            return result;
        }

        public async Task<bool> Add(int id)
        {
            var schedules = await GetSchedules();
            bool exists = schedules.Any(x => x.ScheduleId == id);
            if (!exists)
            {
                var newSchedule = new Schedule(id, GenerateDefaultDays());
               var result =  await repository.Add(newSchedule);
               return (result.Success);
            }
            return false;
        }

        private List<DaySchedule> GenerateDefaultDays()
        {
            var defaultDays = new List<DaySchedule>();

            for (int dayNumber = 1; dayNumber <= 6; dayNumber++) 
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