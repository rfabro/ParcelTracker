using System.ComponentModel.DataAnnotations;

namespace ParcelTracker.Application.Features.Rules.Models;

public class RuleRequest : IValidatableObject
{
    public int ClientId { get; set; }
    public string RuleName { get; set; }
    public string RuleDescription { get; set; }
    public string DefaultEmailFrom { get; set; }
    public string DefaultEmailTo { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if ((ClientId != 1) && ClientId != 2)
            yield return new ValidationResult($"{nameof(ClientId)} must be either 1 or 2. Provided value is {ClientId}");

        if (string.IsNullOrEmpty(RuleName))
            yield return new ValidationResult($"{nameof(RuleName)} must have a value");

        if (string.IsNullOrEmpty(RuleDescription))
            yield return new ValidationResult($"{nameof(RuleDescription)} must have a value");

        if (string.IsNullOrEmpty(DefaultEmailFrom))
            yield return new ValidationResult($"{nameof(DefaultEmailFrom)} must have a value");

        if (string.IsNullOrEmpty(DefaultEmailTo))
            yield return new ValidationResult($"{nameof(DefaultEmailTo)} must have a value");
    }
}