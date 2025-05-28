using ScheduleBackend.Services.Interfaces;

namespace ScheduleBackend.Services
{
    public class PasswordService : IPasswordService
    {
        public string Generatehash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool Generatehash(string password, string hash) =>  BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
