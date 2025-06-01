using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Models.Dto
{
    public class LoginRequest
    {
        [JsonPropertyName("login")]
        [Required]
        public string Login { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        /// <summary>
        /// JWT-токен для последующих запросов
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }

        /// <summary>
        /// Время жизни токена в днях
        /// </summary>
        [JsonPropertyName("expiresInDays")]
        public int ExpiresIn { get; set; }

    }


    public record LoginUserInfo(Guid Id, string Login, string Password, UserRole Role);
}
