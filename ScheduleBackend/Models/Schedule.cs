namespace ScheduleBackend.Models
{
    /// <summary>
    /// Корневой объект для представления расписания пользователей.
    /// </summary>
    public class RootObject
    {
        /// <summary>
        /// Список расписаний пользователей.
        /// </summary>
        public List<Schedule> UsersSchedules { get; set; }
    }

    /// <summary>
    /// Представляет одно расписание для пользователя.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// Уникальный идентификатор расписания.
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Список расписаний на каждый день в рамках данного расписания.
        /// </summary>
        public List<DaySchedule> Days { get; set; }
    }

    /// <summary>
    /// Представляет одно занятие с его деталями.
    /// </summary>
    public class Activity
    {
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
        /// Статус бронирования занятия (занято или свободно).
        /// </summary>
        public bool IsBooked { get; set; }

        /// <summary>
        /// Название курса или занятия. Может быть пустым, если не задано.
        /// </summary>
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
        public int DayNumber { get; set; }

        /// <summary>
        /// Список занятий для данного дня.
        /// </summary>
        public List<Activity> Activities { get; set; }
    }

    /// <summary>
    /// Запрос для обновления информации о занятии в расписании.
    /// </summary>
    public class ScheduleUpdateRequest
    {
        /// <summary>
        /// ID расписания, которое нужно обновить.
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Номер дня в расписании, в котором нужно обновить занятие.
        /// </summary>
        public int DayNumber { get; set; }

        /// <summary>
        /// Номер занятия, которое нужно обновить.
        /// </summary>
        public int ActivityNumber { get; set; }

        /// <summary>
        /// Новое название курса для занятия, или 0, если запись отменена.
        /// </summary>
        public string? NewLessonName { get; set; }

        /// <summary>
        /// Статус бронирования занятия.
        /// </summary>
        public bool IsBooked { get; set; }
    }

    /// <summary>
    /// Запрос для проверки наличия занятых мест в расписании.
    /// </summary>
    public class ScheduleCheckRequest
    {
        /// <summary>
        /// ID расписания для проверки.
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Номер дня в расписании для проверки.
        /// </summary>
        public int DayNumber { get; set; }

        /// <summary>
        /// Номер занятия для проверки.
        /// </summary>
        public int ActivityNumber { get; set; }
    }
}
