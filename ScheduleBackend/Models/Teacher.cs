namespace ScheduleBackend.Models
{
    /// <summary>
    /// Представляет учителя в системе.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Уникальный идентификатор учителя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер группы учителя. (Например, 1 - учителя рисования, 2 - пения и тд)
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Имя пользователя учителя (используется для аутентификации).
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Количество активных слотов у учителя (количество доступных времен для занятий).
        /// </summary>
        public int ActiveSlots { get; set; }
    }

    public class TeacherUpdateRequest
    {
        /// <summary>
        /// Уникальный идентификатор учителя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Если 0 - прибавляем 1, если - 1 - отнимаем (инкримент и декримент)
        /// </summary>
        public int Action { get; set; }
    }

    public class TeacherUpdateResponse
    {
        /// <summary>
        /// Учитель
        /// </summary>
        public Teacher Teacher { get; set; }

        /// <summary>
        /// сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Результат
        /// </summary>
        public bool Result { get; set; }

        public TeacherUpdateResponse(Teacher teacher, string message, bool result)
        {
            Teacher = teacher;
            Message = message;
            Result = result;
        }
    }

    /// <summary>
    /// Представляет одно расписание занятий (курс) для преподавателя.
    /// </summary>
    public class TeacherSchedule
    {
        /// <summary>
        /// Уникальный идентификатор расписания.
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Список расписаний на каждый день в рамках данного расписания.
        /// </summary>
        public List<Lesson> Lessons { get; set; }
    }
}