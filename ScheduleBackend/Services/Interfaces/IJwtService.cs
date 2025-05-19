using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Services.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Генерирует JWT-токен на основе ID пользователя и его роли.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <param name="role">Роль пользователя в системе.</param>
        /// <returns>JWT-токен в виде строки.</returns>
        (string token, int expiresInDays) GenerateToken(Guid id, string role);

        /// <summary>
        /// Валидирует и расшифровывает JWT-токен.
        /// </summary>
        /// <param name="token">JWT-токен в виде строки.</param>
        /// <returns>
        /// Объект TokenData с ID и ролью пользователя, если токен валиден; иначе null.
        /// </returns>
        TokenData? ValidateToken(string token);
    }
    public class TokenData
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
    }

}
