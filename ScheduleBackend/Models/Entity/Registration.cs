using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScheduleBackend.Models.Dto;

namespace ScheduleBackend.Models.Entity
{
    public class Registration
    {
        /// <summary>
        /// Уникальный идентификатор заявки.
        /// </summary>
        [Key]
        public Guid Id { get; set; }


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
        /// Электронная почта
        /// </summary>
        [EmailAddress]
        [MaxLength(100)]
        [Required]
        public required string Email { get; set; }


        /// <summary>
        /// Описание заявки.
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Дата создания заявки.
        /// </summary>
        [Required]
        public DateTime CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

        /// <summary>
        /// Статус заявки (новая, в обработке, завершена и т.д.).
        /// </summary>
        [Required]
        public RegistrationStatus Status { get; set; } = RegistrationStatus.New;

        /// <summary>
        /// Id администратора, к которому относится заявка (внешний ключ).
        /// </summary>

        public Guid? AdminId { get; set; }

        /// <summary>
        /// Навигационное свойство к администратору.
        /// </summary>
        [ForeignKey("AdminId")]
        public Admin? Admin { get; set; }





        public static Registration Create(RegistrationCreateRequest request)
        {
            return new Registration()
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                Email = request.Email,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName
            };
        }


    }

    public enum RegistrationStatus
    {
        New = 0,
        InProgress = 1,
        Done = 2,
        Rejected = 3
    }
}