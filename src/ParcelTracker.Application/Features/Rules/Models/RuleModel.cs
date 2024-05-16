namespace ParcelTracker.Application.Features.Rules.Models;

public class RuleModel
{
    public int ClientId { get; set; }

    public string Rule { get; set; }

    public string RuleDescription { get; set; }

    public string DefaultEmailFrom { get; set; }

    public string DefaultEmailTo { get; set; }
}