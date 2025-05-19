using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface ITeacherScheduleRepository
    {
        Task<IEnumerable<TeacherSchedule>> GetAllSchedules();
        Task<TeacherSchedule?> GetScheduleByTeacherId(Guid teacherId);
        Task<IEnumerable<Lesson>> GetLessons(Guid teacherId);
    }
}
