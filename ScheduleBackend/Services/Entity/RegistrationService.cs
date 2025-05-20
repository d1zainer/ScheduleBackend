using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Models.Messages;
using ScheduleBackend.Repositories;
using ScheduleBackend.Repositories.Interfaces;

namespace ScheduleBackend.Services.Entity
{
    public class RegistrationService(IRegistrationRepository repository)
    {

        public async Task<(bool succes, Exception? ex)> CreateNewRegistration(RegistrationCreateRequest request)
        {
                var registration = Registration.Create(request);
                return await repository.Add(registration);
        }


        public async Task<(bool succes, Exception? exa, Registration? updated)> UpdateRegistrationStatus(
            RegistrationStatusUpdateRequest request)
        {

            var id = request.Guid;
            var adminId = request.AdminGuid;
            var status = request.NewStatus;
            return await repository.UpdateStatus(id, (RegistrationStatus)status, adminId);
        }
        public async Task<(bool success, IEnumerable<Registration>? data)> GetAll(RegistrationStatus? status)
        {
            return await repository.GetAll(status);
        }



      




        public async Task<Registration?> GetById(Guid id)
        {
            return await repository.GetById(id);
        }

        public async Task<(bool Success, Exception? Ex, Registration? Updated)> Update(Guid id, Registration updated)
        {
            return await repository.Update(id, updated);
        }

    }
}
