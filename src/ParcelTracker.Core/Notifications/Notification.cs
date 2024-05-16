namespace ParcelTracker.Core.Notifications;

public class Notification
{
    public int ClientId { get; set; }
    public string ReferenceId { get; set; }
    public NotificationType NotificationType { get; set; }
}