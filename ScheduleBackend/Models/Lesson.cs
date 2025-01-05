namespace ScheduleBackend.Models
{

    public class Lesson
    {
        /// <summary>
        /// Номер дня в расписании.
        /// </summary>
        public int DayNumber { get; set; }

        /// <summary>
        /// Номер занятия в рамках дня.
        /// </summary>
        public int ActivityNumber { get; set; }

        /// <summary>
        /// Время начала занятия.
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// Время окончания занятия.
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// Название курса или занятия. Может быть пустым, если не задано.
        /// </summary>
        public string? LessonName { get; set; }
    }
}
