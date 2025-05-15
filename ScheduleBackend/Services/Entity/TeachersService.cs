using Newtonsoft.Json;
using ScheduleBackend.Models;
using ScheduleBackend.Repositories.Interfaces;

namespace ScheduleBackend.Services.Entity
{
    public class TeachersService(ITeacherRepository repository)
    {
        private readonly ITeacherRepository _repository;

        public async Task<IEnumerable<Teacher>> GetAll() => await repository.GetAll();
        public async Task<(bool Success, Exception? Ex)> Add(Teacher teacher) => await _repository.Add(teacher);
        public async Task<(bool Success, Exception? Ex)> Delete(int id) => await _repository.Delete(id);

        public async Task<int?> GetActiveSlots(int id)
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