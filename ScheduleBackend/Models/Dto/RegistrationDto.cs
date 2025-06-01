using System.ComponentModel.DataAnnotations;
using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Models.Dto
{
    public class RegistrationCreateRequest
    {
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
    }



    public class RegistrationStatusUpdateRequest
    {
        /// <summary>
        /// Id Админа
        /// </summary>
        [Required]
        public required Guid Guid { get; set; }
        /// <summary>
        /// Id Админа
        /// </summary>
        [Required]
        public required Guid AdminGuid { get; set; }
        /// <summary>
        /// Новый статус
        /// </summary>
        [Required]
        public required int NewStatus { get; set; }
    }


    public class RegistrationSortRequest
    {
        /// <summary>
        /// параметр сортировки
        /// </summary>
        public RegistrationStatus? SortByStatus { get; set; }
    }

}
