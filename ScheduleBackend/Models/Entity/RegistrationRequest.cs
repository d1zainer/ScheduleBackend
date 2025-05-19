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
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Статус заявки (новая, в обработке, завершена и т.д.).
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        /// <summary>
        /// Id администратора, к которому относится заявка (внешний ключ).
        /// </summary>
        [Required]
        public Guid AdminId { get; set; }

        /// <summary>
        /// Навигационное свойство к администратору.
        /// </summary>
        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }
    }
}