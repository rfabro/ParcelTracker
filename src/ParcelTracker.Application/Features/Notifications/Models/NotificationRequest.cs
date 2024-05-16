using System.ComponentModel.DataAnnotations;

namespace ParcelTracker.Application.Features.Notifications.Models;

public class NotificationRequest : IValidatableObject
{
    public int ClientId { get; set; }
    public string ReferenceId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if ((ClientId != 1) && ClientId != 2)
            yield return new ValidationResult($"{nameof(ClientId)} must be either 1 or 2. Provided value is {ClientId}");

        if (string.IsNullOrEmpty(ReferenceId))
            yield return new ValidationResult($"{nameof(ReferenceId)} must have a value");
    }
}