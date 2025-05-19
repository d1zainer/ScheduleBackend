using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace ScheduleBackend.Models.Entity
{
    /// <summary>
    /// Представляет одно расписание для пользователя.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// Уникальный идентификатор расписания.
        /// </summary>
        [Key]
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// Список расписаний на каждый день в рамках данного расписания.
        /// </summary>
        [Required]
        public List<DaySchedule> Days { get; set; }


        /// <summary>
        /// Навигационное свойство к владельцу расписания (пользователю).
        /// </summary>
        public Student User { get; set; }

        public Schedule(Guid scheduleId, List<DaySchedule> days)
        {
            ScheduleId = scheduleId;
            Days = days;
        }
        public Schedule() { }
    }

    /// <summary>
    /// Представляет одно занятие с его деталями.
    /// </summary>
    public class Activity
    {

        /// <summary>
        /// Номер занятия в рамках дня.
        /// </summary>
        [Range(1, 20)]
        
        public int ActivityNumber { get; set; }

        /// <summary>
        /// Время начала занятия.
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания занятия.
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Статус бронирования занятия.
        /// </summary>
        public bool IsBooked { get; set; }

        /// <summary>
        /// Название курса или занятия. Может быть пустым.
        /// </summary>
        [MaxLength(100)]
        public string? LessonName { get; set; }
    }

    /// <summary>
    /// Представляет расписание для одного дня.
    /// </summary>
    public class DaySchedule
    {
        /// <summary>
        /// Номер дня в расписании.
        /// </summary>
        [Range(1, 7)]
        public int DayNumber { get; set; }

        /// <summary>
        /// Список занятий для данного дня.
        /// </summary>
        [Required]
        public List<Activity> Activities { get; set; }
    }

    /// <summary>
    /// Класс для проверки наличия занятых мест в расписании.
    /// </summary>
    public class ActivityCheck
    {
        /// <summary>
        /// ID расписания для проверки.
        /// </summary>
        [Required]
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// Номер дня в расписании для проверки.
        /// </summary>
        [Range(1, 7)]
        public int DayNumber { get; set; }

        /// <summary>
        /// Номер занятия для проверки.
        /// </summary>
        [Range(1, 20)]
        public int ActivityNumber { get; set; }

        /// <summary>
        /// Название курса или занятия. Может быть пустым.
        /// </summary>
        [MaxLength(100)]
        public string? LessonName { get; set; } = null;
    }
}
