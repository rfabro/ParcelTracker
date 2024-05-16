using ParcelTracker.Application.Features.Notifications.Models;
using ParcelTracker.Core.Notifications;

namespace ParcelTracker.Application.Features.Notifications.Services;

public interface INotificationService
{
    Task<IEnumerable<NotificationModel>> GetAllByClientId(string clientId, bool includesDeleted);
    Task<NotificationModel> CreateNotification(Notification notification);
    Task<NotificationModel> CreateDelivery(int clientId, string referenceId);
    Task<NotificationModel> CreatePickup(int clientId, string referenceId);
    Task<NotificationModel> CreateReminder(int clientId, string referenceId);
}