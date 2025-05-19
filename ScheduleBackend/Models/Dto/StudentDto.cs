using System.ComponentModel.DataAnnotations;

namespace ScheduleBackend.Models.Dto
{
    /// <summary>
    /// Запрос для авторизации пользователя.
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// Имя пользователя для входа в систему.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя для входа в систему.
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// Ответ, возвращаемый после успешной авторизации пользователя.
    /// </summary>
    public class UserLoginResponse
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Конструктор для создания ответа с идентификатором пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public UserLoginResponse(Guid id)
        {
            Id = id;
        }
    }










    public class StudentCreateResponse
    {
        [Required]
        public required string Login { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string MiddleName { get; set; }

        [Required]
        public required  DateTime DateOfBirth { get; set; }
   
        public string? Email { get; set; }
     
        public string? PhoneNumber { get; set; }
    }

}
