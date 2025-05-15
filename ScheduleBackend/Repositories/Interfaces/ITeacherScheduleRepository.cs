using ScheduleBackend.Models;

namespace ScheduleBackend.Repositories.Interfaces
{
    public interface ITeacherScheduleRepository
    {
        Task<IEnumerable<TeacherSchedule>> GetAllSchedules();
        Task<TeacherSchedule?> GetScheduleByTeacherId(int teacherId);
        Task<IEnumerable<Lesson>> GetLessons(int teacherId);
    }
}
