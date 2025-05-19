using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Models.Settings;
using ScheduleBackend.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ScheduleBackend.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtService(IOptions<JwtSettings> options, TokenValidationParameters tokenValidationParameters)
        {
            _jwtSettings = options.Value;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public (string token, int expiresInDays) GenerateToken(Guid id, string role)
        {
            var claims = new List<Claim>
            {
                new Claim("guid", id.ToString()),
                new Claim("role", role)
            };

            int tokenLifetimeDays = 30; // или брать из _jwtSettings, если там есть
            var expiry = DateTime.UtcNow.AddDays(tokenLifetimeDays);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiry,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (token, tokenLifetimeDays);
        }

        public TokenData? ValidateToken(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

                // Проверяем, что алгоритм подписи ожидаемый
                if (validatedToken is JwtSecurityToken jwtToken &&
                    jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Получаем ID пользователя из claim с типом Sub
                    var idClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                    // Получаем роль из claim с типом Role
                    var roleClaim = principal.FindFirst(ClaimTypes.Role)?.Value;

                    if (Guid.TryParse(idClaim, out var userId))
                    {
                        return new TokenData
                        {
                            Id = userId,
                            Role = Enum.Parse<UserRole>(roleClaim, ignoreCase: true)
                        };
                    }
                }
            }
            catch
            {
                // Можно логировать ошибку валидации
            }

            return null;
        }
    }
}
