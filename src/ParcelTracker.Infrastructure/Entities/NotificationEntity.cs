using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParcelTracker.Core.Abstractions;

namespace ParcelTracker.Infrastructure.Entities;

[Table("Notifications")]
public class NotificationEntity : Entity
{
    [Key]
    [Required]
    public long NotificationId { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    public int NotificationType { get; set; }

    [Required]
    public string ReferenceId { get; set; }

    public DateTime DateCreated { get; set; }
}