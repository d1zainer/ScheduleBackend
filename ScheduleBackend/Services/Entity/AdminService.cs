using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Models.Messages;
using ScheduleBackend.Repositories;
using ScheduleBackend.Repositories.Db;
using ScheduleBackend.Repositories.Interfaces;
using ScheduleBackend.Services.Interfaces;

public class AdminService(IAdminRepository adminRepository, INotificationSender sender, IUserRepository userRepository, IPasswordService passwordService)
{
    public async Task<(bool Success, Exception? Ex)> Add(AdminDto dto)
    {
        try
        {
            var login = dto.Login;
            var existUser = await userRepository.GetUserByLoginAsync(login);

            if (existUser is null) return (false, new Exception("Админ с таким логином уже сущетсвует!"));
            

            var admin = Admin.Create(dto, passwordService.Generatehash(dto.Password));
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
        
        return await adminRepository.Update(Admin.Create(updated, passwordService.Generatehash(updated.Password)));
    }

    public async Task<(bool Success, Exception? Ex)> Delete(Guid id)
    {
        return await adminRepository.Delete(id);
    }
}