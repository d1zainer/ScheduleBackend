using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        /// Описание заявки.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Дата создания заявки.
        /// </summary>
        [Required]
        public DateTime CreatedAt => DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

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
    }

    public enum RegistrationStatus
    {
        New = 0,
        InProgress = 1,
        Done = 2,
        Rejected = 3
    }
}