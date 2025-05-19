using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScheduleBackend.Models.Dto;

namespace ScheduleBackend.Models.Entity
{
    /// <summary>
    /// Представляет администратора в системе.
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Уникальный идентификатор администратора.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Логин администратора для аутентификации.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Login { get; set; }

        /// <summary>
        /// Пароль администратора (в зашифрованном виде).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Password { get; set; }

        /// <summary>
        /// Роль пользователя (всегда должна быть Admin для этой сущности).
        /// </summary>
        [Required]
        public UserRole Role { get; set; } = UserRole.Admin;

        /// <summary>
        /// Фамилия администратора.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        /// <summary>
        /// Имя администратора.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        /// <summary>
        /// Отчество администратора.
        /// </summary>
        [MaxLength(100)]
        public string? MiddleName { get; set; }

        /// <summary>
        /// Электронная почта администратора.
        /// </summary>
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Номер телефона администратора.
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Список заявок, обработкой которых занимается администратор.
        /// </summary>
        public List<Registration> Registration { get; set; } = new();

        /// <summary>
        /// Создаёт объект Admin на основе AdminDto.
        /// Роль устанавливается как Admin по умолчанию.
        /// </summary>
        public static Admin Create(AdminDto dto)
        {
            return new Admin
            {
                Id = Guid.NewGuid(),
                Login = dto.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = UserRole.Admin,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
        }
    }
}
