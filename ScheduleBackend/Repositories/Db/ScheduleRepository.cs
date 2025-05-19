using ScheduleBackend.Models.Entity;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services;

namespace ScheduleBackend.Repositories.Db
{
    public class ScheduleRepository : IScheduleRepository
    {

        public async Task<(bool Success, Exception? Ex)> Add(Schedule schedule)
        {
            try
            {
                var schedules = await LoadAll();
                schedules.Add(schedule);
                await SaveAll(schedules);
                return (true, null);
            }
            catch (Exception ex) { return (false, ex); }

        }

        private async Task<List<Schedule>> LoadAll() => await JsonService.GetJson<Schedule>(JsonService.UsersSchedules);
     
        private async Task SaveAll(List<Schedule> schedules) => await JsonService.Save(schedules, JsonService.UsersSchedules);


        public async Task<(bool Success, Exception? Ex)> Delete(Guid id)
        {
            try
            {
                var schedules= await LoadAll();
                var schedule = schedules.FirstOrDefault(x => x.ScheduleId == id);
                if (schedule == null)
                    return (false, null);

                schedules.Remove(schedule);
                await SaveAll(schedules);
                return (true, null);
            }
            catch (Exception ex) { return (false, ex); }
        }

        public async Task<IEnumerable<Schedule>> GetAll() =>  await LoadAll();
    

        public async Task<Schedule?> GetById(Guid id)
        {
            var schedules = await LoadAll();
            return schedules.FirstOrDefault(x => x.ScheduleId == id);
        }

        public async Task<(bool Success, Exception? Ex, Schedule? Updated)> Update(Schedule schedule)
        {
            try
            {
                var schedules = await LoadAll();
                var index = schedules.FindIndex(x => x.ScheduleId == schedule.ScheduleId);
                if (index == -1)
                    return (false, null, null);

                schedules[index] = schedule;
                await SaveAll(schedules);
                return (true, null, schedule);
            }
            catch (Exception ex) { return (false, ex, null); }
        }
    }
}
