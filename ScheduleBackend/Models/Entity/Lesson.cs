using System.ComponentModel.DataAnnotations;

namespace ScheduleBackend.Models.Entity
{
    public class Lesson
    {
        /// <summary>
        /// Номер дня в расписании.
        /// </summary>
        [Range(1, 7, ErrorMessage = "DayNumber должен быть от 1 до 7.")]
        public int DayNumber { get; set; }

        /// <summary>
        /// Номер занятия в рамках дня.
        /// </summary>
        [Range(1, 20, ErrorMessage = "ActivityNumber должен быть от 1 до 20.")]
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
        /// Название курса или занятия. Может быть пустым, если не задано.
        /// </summary>
        [MaxLength(100)]
        public string? LessonName { get; set; }
    }
}