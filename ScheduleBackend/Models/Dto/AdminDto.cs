using System.ComponentModel.DataAnnotations;

namespace ScheduleBackend.Models.Dto
{
    public class AdminDto
    {
        [Required(ErrorMessage = "Логин обязателен.")]
        [MaxLength(100)]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Пароль обязателен.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна.")]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Имя обязательно.")]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [MaxLength(100)]
        public required string MiddleName { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Phone(ErrorMessage = "Некорректный номер телефона.")]
        [MaxLength(20)]
        public required string PhoneNumber { get; set; }
    }
}
