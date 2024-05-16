using AutoMapper;
using Microsoft.Extensions.Logging;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Core.Rules;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application.Features.Rules.Services;

public class RuleService : IRuleService
{
    private readonly ILogger<RuleService> _logger;
    private readonly IAsyncRepository<RuleEntity> _repository;
    private readonly IMapper _mapper;
    private const string ModuleName = nameof(RuleRepository);

    public RuleService(ILogger<RuleService> logger, IAsyncRepository<RuleEntity> repository, IMapper mapper)
    {
        _logger = logger;
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
        {
            _logger.LogError($"{ModuleName}: CreateRule: rule is null");
            return rule;
        }

        var newEntity = _mapper.Map<RuleEntity>(rule);
        await _repository.AddAsync(newEntity);
        return _mapper.Map<Rule>(rule);
    }

    public async Task<Rule> UpdateRule(Rule rule)
    {
        if (rule == null)
        {
            _logger.LogError($"{ModuleName}: UpdateRule: rule is null");
            return rule;
        }

        var existingEntity = _mapper.Map<RuleEntity>(rule);
        var existingResult = await _repository.GetAsync(c => c.ClientId == existingEntity.ClientId);

        if (existingResult != null)
        {
            await _repository.UpdateAsync(existingResult);
            return _mapper.Map<Rule>(existingResult);
        }

        return rule;
    }
}