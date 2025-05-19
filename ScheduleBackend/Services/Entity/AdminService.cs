using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Models.Messages;
using ScheduleBackend.Repositories;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services.Interfaces;

public class AdminService(IAdminRepository adminRepository, INotificationSender sender)
{
    public async Task<(bool Success, Exception? Ex)> Add(AdminDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Login))
                throw new ArgumentException("Username обязателен.");
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password обязателен.");
            if (string.IsNullOrWhiteSpace(dto.FirstName))
                throw new ArgumentException("FirstName обязателен.");
            if (string.IsNullOrWhiteSpace(dto.LastName))
                throw new ArgumentException("LastName обязателен.");

            var admin = Admin.Create(dto);
            var newAdmin = await adminRepository.Add(admin);

            if (newAdmin.Success)
            {
                await sender.PublishEmailAsync(new UserCreateData()
                {
                    Email = dto.Email,
                    Body = $"Пароль - {dto.Password}, логин - {dto.Login}",
                    Subject = "Регистрация"
                });
            }

            return newAdmin;
        }
        catch (Exception ex) {   return (false, ex); }
    }

    public async Task<IEnumerable<Admin>> GetAll()
    {
        return await adminRepository.GetAll();
    }

    public async Task<Admin?> GetById(Guid id)
    {
        return await adminRepository.GetById(id);
    }

    public async Task<(bool Success, Exception? Ex, Admin? Updated)> Update(Guid id, AdminDto updated)
    {
        
        return await adminRepository.Update(Admin.Create(updated));
    }

    public async Task<(bool Success, Exception? Ex)> Delete(Guid id)
    {
        return await adminRepository.Delete(id);
    }
}