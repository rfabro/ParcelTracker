using AutoMapper;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Application.Features.Rules.Services;
using ParcelTracker.Core.Rules;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application.Features.Configuration.Services;

public class RuleService : IRuleService
{
    private readonly IAsyncRepository<RuleEntity> _repository;
    private readonly IMapper _mapper;

    public RuleService(IAsyncRepository<RuleEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Rule>> GetAllRules()
    {
        var result = await _repository.GetAllAsync();
        var mappedResult = result.Select(n => _mapper.Map<Rule>(n));
        return mappedResult;
    }

    public async Task<IEnumerable<Rule>> GetAllByClientId(int clientId)
    {
        var result = await _repository.GetAllAsync(n => n.ClientId == clientId);
        var mappedResult = result.Select(n => _mapper.Map<Rule>(n));
        return mappedResult;
    }

    public async Task<Rule> CreateRule(Rule rule)
    {
        if (rule == null)
            return null;

        var newEntity = _mapper.Map<RuleEntity>(rule);
        await _repository.AddAsync(newEntity);
        return _mapper.Map<Rule>(rule);
    }

    public async Task<Rule> UpdateConfiguration(Rule rule)
    {
        if (rule == null)
            return null;

        var existingEntity = _mapper.Map<RuleEntity>(rule);
        var existingResult = await _repository.GetAsync(c => c.ClientId == existingEntity.ClientId);

        if (existingResult != null)
        {
            await _repository.UpdateAsync(existingResult);
            return _mapper.Map<Rule>(existingResult);
        }

        return null;
    }
}