namespace ScheduleBackend.Services.Interfaces;

public interface IPasswordService
{
    string Generatehash(string password);
    bool Generatehash(string password, string hash);
}