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
        /// Имя пользователя учителя (используется для аутентификации).
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Количество активных слотов у учителя (количество доступных времен для занятий).
        /// </summary>
        public int ActiveSlots { get; set; }
    }
}