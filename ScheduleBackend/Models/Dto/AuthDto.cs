using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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


    public class UnifiedUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = null!;
    }

}
