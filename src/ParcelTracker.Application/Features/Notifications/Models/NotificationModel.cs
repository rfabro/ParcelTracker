using ParcelTracker.Core.Notifications;

namespace ParcelTracker.Application.Features.Notifications.Models;

public class NotificationModel
{
    public int ClientId { get; set; }
    public string ReferenceId { get; set; }
    public NotificationType NotificationType { get; set; }
}