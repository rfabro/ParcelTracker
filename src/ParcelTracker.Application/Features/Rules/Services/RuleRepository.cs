using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Infrastructure.Contexts;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application.Features.Rules.Services;

public class RuleRepository : IAsyncRepository<RuleEntity>
{
    private readonly ILogger<RuleRepository> _logger;
    private readonly RulesContext _dbContext;
    private const string ModuleName = nameof(RuleRepository);

    public RuleRepository(ILogger<RuleRepository> logger, RulesContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<RuleEntity> GetAsync(Expression<Func<RuleEntity, bool>> predicate)
    {
        try
        {
            var result = await _dbContext.Rules.FirstOrDefaultAsync(predicate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on GetAsync. {ex?.Message ?? ex.InnerException?.Message}");
            throw;
        }
    }

    public async Task<List<RuleEntity>> GetAllAsync()
    {
        try
        {
            var result = await _dbContext.Rules.ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on GetAllAsync. {ex?.Message ?? ex.InnerException?.Message}");
            throw;
        }
    }

    public async Task<List<RuleEntity>> GetAllAsync(Expression<Func<RuleEntity, bool>> predicate)
    {
        try
        {
            var result = await _dbContext.Rules.Where(predicate).ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on GetAllAsync. {ex?.Message ?? ex.InnerException?.Message}");
            throw;
        }
    }

    public async Task AddAsync(RuleEntity entity)
    {
        try
        {
            await _dbContext.Rules.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on AddAsync. {ex?.Message ?? ex.InnerException?.Message}");
            throw;
        }
    }

    public async Task UpdateAsync(RuleEntity entity)
    {
        try
        {
            var exists = await _dbContext.Rules.FindAsync(entity);
            if (exists != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on UpdateAsync. {ex?.Message ?? ex.InnerException?.Message}");
            throw;
        }
    }
}