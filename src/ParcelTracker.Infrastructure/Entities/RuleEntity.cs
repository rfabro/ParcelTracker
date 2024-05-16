using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParcelTracker.Core.Abstractions;

namespace ParcelTracker.Infrastructure.Entities;

[Table("Rules")]
public class RuleEntity : Entity
{
    [Key]
    [Required]
    public long RuleId { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    public string RuleName { get; set; }

    public string RuleDescription { get; set; }

    [Required]
    public string DefaultEmailFrom { get; set; }

    [Required]
    public string DefaultEmailTo { get; set; }
}