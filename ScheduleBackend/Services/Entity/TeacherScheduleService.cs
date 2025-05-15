using Newtonsoft.Json;
using ScheduleBackend.Models;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Repositories.Json;

namespace ScheduleBackend.Services.Entity
{
    public class TeacherScheduleService(ITeacherScheduleRepository repository)
    {
        public async Task<IEnumerable<TeacherSchedule>> GetTeachersSchedules() => await repository.GetAllSchedules();
        public async Task<IEnumerable<Lesson>> GetLessons(int teacherId) => await repository.GetLessons(teacherId);

    }
}