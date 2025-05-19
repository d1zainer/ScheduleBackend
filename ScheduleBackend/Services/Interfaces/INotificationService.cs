using ScheduleBackend.Models.Entity;
using ScheduleBackend.Models.Messages;

namespace ScheduleBackend.Services.Interfaces;

public interface INotificationSender
{
    Task PublishEmailAsync(UserCreateData data, CancellationToken ct = default);
}