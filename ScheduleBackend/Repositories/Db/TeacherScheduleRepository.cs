﻿using Newtonsoft.Json;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services;

namespace ScheduleBackend.Repositories.Db
{
    public class TeacherScheduleRepository : ITeacherScheduleRepository
    {
        private readonly string _jsonFilePath = JsonService.TeachersSchedules;
        public async Task<IEnumerable<TeacherSchedule>> GetAllSchedules()
        {
            var result = await JsonService.GetJson<TeacherSchedule>(_jsonFilePath);
            return result;
        }

        public async Task<IEnumerable<Lesson>> GetLessons(Guid teacherId)
        {
            var schedule = await GetScheduleByTeacherId(teacherId);
            if (schedule != null) return schedule.Lessons;
            return new List<Lesson>();
        }

        public async Task<TeacherSchedule?> GetScheduleByTeacherId(Guid teacherId)
        {
            var schedules = await GetAllSchedules();
            return schedules.FirstOrDefault(s => s.TeacherId == teacherId);
        }

        private async Task<(bool Success, Exception? Ex)> SaveAll(List<TeacherSchedule> schedules)
        {
            try
            {
                var json = JsonConvert.SerializeObject(schedules, Formatting.Indented);
                await File.WriteAllTextAsync(_jsonFilePath, json);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }
    }
}
