namespace ParcelTracker.Core.Rules;

public class Rule
{
    public int ClientId { get; set; }

    public string RuleName { get; set; }

    public string RuleDescription { get; set; }

    public string DefaultEmailFrom { get; set; }

    public string DefaultEmailTo { get; set; }
}