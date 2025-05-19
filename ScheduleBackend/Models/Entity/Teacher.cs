using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Services.Entity;

namespace ScheduleBackend.Models.Entity
{

    /// <summary>
    /// Представляет учителя в системе.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Уникальный идентификатор учителя.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Номер группы учителя.
        /// </summary>
        [Required]
        public required int GroupId { get; set; }

        /// <summary>
        /// Логин учителя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Login { get; set; }

        /// <summary>
        /// Пароль учителя (в зашифрованном виде).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Password { get; set; }

        /// <summary>
        /// Количество активных слотов у учителя.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int ActiveSlots { get; set; }

        /// <summary>
        /// Фамилия учителя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        /// <summary>
        /// Имя учителя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        /// <summary>
        /// Отчество учителя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string MiddleName { get; set; }


        /// <summary>
        /// Роль пользователя (всегда должна быть Admin для этой сущности).
        /// </summary>
        [Required]
        public required UserRole Role { get; set; } = UserRole.Teacher;

        /// <summary>
        /// Email учителя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }


        /// <summary>
        /// Номер телефона учителя (необязательное поле).
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        // Навигация (если есть TeacherSchedule)
        public ICollection<TeacherSchedule> TeacherSchedules { get; set; } = new List<TeacherSchedule>();


        public static Teacher Create(TeacherCreateRequest request)
        {
            
            return new Teacher()
            {
                Id = Guid.NewGuid(),
                Login = request.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = UserRole.User,
                MiddleName = request.MiddleName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                GroupId = request.GroupId,
                ActiveSlots = 0,
                TeacherSchedules = new List<TeacherSchedule>()
            };
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
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Внешний ключ к учителю.
        /// </summary>
        [ForeignKey("Teacher")]
        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        /// <summary>
        /// Список расписаний на каждый день в рамках данного расписания.
        /// </summary>
        public List<Lesson> Lessons { get; set; }
    }
}
