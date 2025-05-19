using Newtonsoft.Json;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Models.Messages;
using ScheduleBackend.Repositories.Db;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services.Interfaces;

namespace ScheduleBackend.Services.Entity
{
    public class TeachersService(ITeacherRepository repository, INotificationSender sender)
    {
        private readonly ITeacherRepository _repository;

        public async Task<IEnumerable<Teacher>> GetAll() => await repository.GetAll();

        public async Task<(bool Success, Exception? Ex)> Add(TeacherCreateRequest dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Login))
                    throw new ArgumentException("Login обязателен.");
                if (string.IsNullOrWhiteSpace(dto.Password))
                    throw new ArgumentException("Password обязателен.");
                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    throw new ArgumentException("FirstName обязателен.");
                if (string.IsNullOrWhiteSpace(dto.LastName))
                    throw new ArgumentException("LastName обязателен.");
                if (string.IsNullOrWhiteSpace(dto.MiddleName))
                    throw new ArgumentException("MiddleName обязателен.");
                if (string.IsNullOrWhiteSpace(dto.Email))
                    throw new ArgumentException("Email обязателен.");

                var teacher = Teacher.Create(dto);

                var newTeacher = await repository.Add(teacher);

                if (newTeacher.Success)
                {
                    await sender.PublishEmailAsync(new UserCreateData()
                    {
                        Email = dto.Email,
                        Body = $"Пароль - {dto.Password}, логин - {dto.Login}",
                        Subject = "Регистрация"
                    });
                }

                return newTeacher;
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public async Task<(bool Success, Exception? Ex)> Delete(Guid id) => await _repository.Delete(id);

        public async Task<int?> GetActiveSlots(Guid id)
        {
            var teacher = await _repository.GetById(id);
            if (teacher == null)
                return null;
            return teacher.ActiveSlots;
        }

        public async Task<TeacherUpdateResponse?> UpdateActiveSlots(TeacherUpdateRequest request)
        {
            var teacher = await _repository.GetById(request.Id);
            if (teacher == null)
                return null;
            if (request.Action == 0)
                teacher.ActiveSlots++;
            else
                teacher.ActiveSlots--;
            var result = await _repository.Update(teacher);
            if (!result.Success)
                return null;
            var message = request.Action == 0
                ? "Количество слотов увеличилось"
                : "Количество слотов уменьшилось";

            return new TeacherUpdateResponse(teacher, message, true);

        }

        public async Task<List<Teacher>> GetTeachersByGroupId(int groupId)
        {
            var teachers = await _repository.GetByGroupId(groupId);
            return teachers.ToList();
        }
    }
}