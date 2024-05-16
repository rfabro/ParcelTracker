using System.ComponentModel.DataAnnotations;

namespace ParcelTracker.Application.Features.Notifications.Models;

public class NotificationRequest : IValidatableObject
{
    public int ClientId { get; set; }
    public string ReferenceId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ClientId <= 0)
            yield return new ValidationResult($"{nameof(ClientId)} has invalid value");

        if (string.IsNullOrEmpty(ReferenceId))
            yield return new ValidationResult($"{nameof(ReferenceId)} must have a value");
    }
}