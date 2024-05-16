using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Infrastructure.Database;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application.Features.Notifications.Services;

public class NotificationRepository : IAsyncRepository<NotificationEntity>
{
    private readonly NotificationsContext _dbContext;

    public NotificationRepository(NotificationsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NotificationEntity> GetAsync(Expression<Func<NotificationEntity, bool>> predicate)
    {
        var result = await _dbContext.Notifications.FirstOrDefaultAsync(predicate);
        return result;
    }

    public async Task<List<NotificationEntity>> GetAllAsync()
    {
        var result = await _dbContext.Notifications.ToListAsync();
        return result;
    }

    public async Task<List<NotificationEntity>> GetAllAsync(Expression<Func<NotificationEntity, bool>> predicate)
    {
        var result = await _dbContext.Notifications.Where(predicate).ToListAsync();
        return result;
    }

    public async Task AddAsync(NotificationEntity entity)
    {
        await _dbContext.Notifications.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}