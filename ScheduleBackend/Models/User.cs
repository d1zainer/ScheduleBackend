namespace ScheduleBackend.Models
{
    /// <summary>
    /// Представляет пользователя в системе.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя для аутентификации.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }
    }

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
        public int Id { get; set; }

        /// <summary>
        /// Конструктор для создания ответа с идентификатором пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public UserLoginResponse(int id)
        {
            Id = id;
        }
    }
}