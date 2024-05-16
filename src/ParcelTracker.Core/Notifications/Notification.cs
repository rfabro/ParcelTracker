namespace ParcelTracker.Core.Notifications;

public class Notification
{
    public int NotificationId { get; set; }
    public int ClientId { get; set; }
    public NotificationType NotificationType { get; set; }
    public string ReferenceId { get; set; }
}