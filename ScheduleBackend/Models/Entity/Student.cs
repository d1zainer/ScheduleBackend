using System.ComponentModel.DataAnnotations;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Services.Entity;

namespace ScheduleBackend.Models.Entity
{

    /// <summary>
    /// Представляет пользователя в системе.
    /// </summary>
    public class Student
    {

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя для аутентификации.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Login { get; set; }

        /// <summary>
        /// Пароль пользователя (в зашифрованном виде).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Password { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string MiddleName { get; set; }
        
        /// <summary>
        /// Электронная почта (необязательно).
        /// </summary>
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Номер телефона (необязательно).
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Роль пользователя (всегда должна быть Admin для этой сущности).
        /// </summary>
        [Required]
        public required UserRole Role { get; set; } = UserRole.User;

        /// <summary>
        /// Внешний ключ на расписание пользователя.
        /// </summary>
        public required Guid ScheduleId { get; set; }

        /// <summary>
        /// Навигационное свойство к расписанию пользователя.
        /// </summary>
        public required Schedule Schedule { get; set; }


        public static Student Create(StudentCreateResponse dto)
        {
           
            var schedule = new Schedule
            {
                ScheduleId = Guid.NewGuid(),
                Days = ScheduleService.GenerateDefaultDays() // пустое расписание
            };

            return new Student
            {
                Id = Guid.NewGuid(),
                Login = dto.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = UserRole.User,
                MiddleName = dto.MiddleName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                ScheduleId = schedule.ScheduleId,
                Schedule = schedule
            };
        }

    }

}