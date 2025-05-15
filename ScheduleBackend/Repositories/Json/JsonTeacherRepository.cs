using Newtonsoft.Json;
using ScheduleBackend.Models;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services;

namespace ScheduleBackend.Repositories.Json
{
    public class JsonTeacherRepository : ITeacherRepository
    {
        private readonly string _jsonFilePath = JsonService.Teachers;

        private async Task<List<Teacher>> LoadAll()
        {
            var result = await JsonService.GetJson<Teacher>(_jsonFilePath);
            return result;
        }

        private async Task SaveAll(List<Teacher> teachers) => await JsonService.Save(teachers, _jsonFilePath);
        
        public async Task<IEnumerable<Teacher>> GetAll() => await LoadAll();
      

        public async Task<Teacher?> GetById(int id)
        {
            var teachers = await LoadAll();
            return teachers.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Teacher>> GetByGroupId(int groupId)
        {
            var teachers = await LoadAll();
            return teachers.Where(x => x.GroupId == groupId);
        }

        public async Task<(bool Success, Exception? Ex)> Add(Teacher teacher)
        {
            try
            {
                var teachers = await LoadAll();
                teachers.Add(teacher);
                await SaveAll(teachers);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public async Task<(bool Success, Exception? Ex)> Delete(int id)
        {
            try
            {
                var teachers = await LoadAll();
                var teacher = teachers.FirstOrDefault(x => x.Id == id);
                if (teacher == null)
                    return (false, null);

                teachers.Remove(teacher);
                await SaveAll(teachers);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public async Task<(bool Success, Exception? Ex, Teacher? Updated)> Update(Teacher teacher)
        {
            try
            {
                var teachers = await LoadAll();
                var index = teachers.FindIndex(x => x.Id == teacher.Id);
                if (index == -1)
                    return (false, null, null);

                teachers[index] = teacher;
                await SaveAll(teachers);
                return (true, null, teacher);
            }
            catch (Exception ex)
            {
                return (false, ex, null);
            }
        }
    }
}
