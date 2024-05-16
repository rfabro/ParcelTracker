namespace ParcelTracker.Application.Features.Notifications.Services;

public interface INotificationService
{
    Task<IEnumerable<Core.Notifications.Notification>> GetAllNotifications();
    Task<IEnumerable<Core.Notifications.Notification>> GetAllByClientId(int clientId);
    Task<Core.Notifications.Notification> CreateNotification(Core.Notifications.Notification notification);
    Task<Core.Notifications.Notification> CreateDelivery(int clientId, string referenceId);
    Task<Core.Notifications.Notification> CreateNewPickup(int clientId, string referenceId);
    Task<Core.Notifications.Notification> CreateReminder(int clientId, string referenceId);
}