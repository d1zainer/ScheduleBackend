using Newtonsoft.Json;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;


namespace ScheduleBackend.Services.Entity
{
    public class TeacherScheduleService(ITeacherScheduleRepository repository)
    {
        public async Task<IEnumerable<TeacherSchedule>> GetTeachersSchedules() => await repository.GetAllSchedules();
        public async Task<IEnumerable<Lesson>> GetLessons(Guid teacherId) => await repository.GetLessons(teacherId);

    }
}