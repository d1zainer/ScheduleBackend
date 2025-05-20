using Newtonsoft.Json;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Models.Messages;
using ScheduleBackend.Repositories.Db;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services.Interfaces;

namespace ScheduleBackend.Services.Entity
{
    public class StudentService(IStudentRepository repository, INotificationSender sender, IJwtService jwtService)
    {


        public async Task<(bool success, Exception? ex, StudentResponse? response)> GetUserByGuid(Guid id)
        {
            try
            {

            
                var findUser = await repository.GetByGuid(id);
                if (findUser is not null)
                {
                    return new(true, null, new()
                    {
                        FirstName = findUser.FirstName,
                        LastName = findUser.LastName,
                        MiddleName = findUser.MiddleName,
                        Email = findUser.Email,
                        PhoneNumber = findUser.PhoneNumber
                    });
                }

                return (false,  new Exception("Юсера с таким айди нет"), null);
            }
            catch (Exception ex)
            {
                return (false, ex, null);
            }
                
        }
        public async Task<List<Student>> GetUsers() => (await repository.GetAll()).ToList();

        public async Task<(bool success, Exception? ex)> Add(StudentCreateResponse dto)
        {
            try
            {
                // Примитивная валидация
                if (string.IsNullOrWhiteSpace(dto.Login))
                    throw new ArgumentException("Username обязателен.");
                if (string.IsNullOrWhiteSpace(dto.Password))
                    throw new ArgumentException("Password обязателен.");
                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    throw new ArgumentException("FirstName обязателен.");
                if (string.IsNullOrWhiteSpace(dto.LastName))
                    throw new ArgumentException("LastName обязателен.");
                if (dto.DateOfBirth == default)
                    throw new ArgumentException("DateOfBirth обязателен.");

                var user = Student.Create(dto);
                var newUser = await repository.Add(user);

                if (newUser.success)
                    await sender.PublishEmailAsync(new UserCreateData()
                    {
                        Email = dto.Email,
                        Body = $"Пароль - {dto.Password}, логин - {dto.Login}",
                        Subject = "Регистрация"
                    });
                

                return newUser;
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        } 
        public async  Task<(bool success, Exception? ex)> Delete(Guid id) => await repository.Delete(id);
        
        public async Task<(Student? user, bool success)> Authenticate(string username, string password)
        {
            var users = await GetUsers();
            var user = users.Find(u => u.Login == username && u.Password == password);
            if (user != null) return (user, true);
            return (null, false);
        }
    }
}