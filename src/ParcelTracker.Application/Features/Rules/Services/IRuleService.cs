using ParcelTracker.Core.Rules;

namespace ParcelTracker.Application.Features.Rules.Services;

public interface IRuleService
{
    Task<IEnumerable<Rule>> GetAllRules();
    Task<IEnumerable<Rule>> GetAllByClientId(int clientId);
    Task<Rule> CreateRule(Rule rule);
    Task<Rule> UpdateRule(Rule rule);
}