﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Infrastructure.Contexts;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application.Features.Notification.Services;

public class NotificationRepository : IAsyncRepository<NotificationEntity>
{
    private readonly ILogger<NotificationRepository> _logger;
    private readonly NotificationsContext _dbContext;

    public NotificationRepository(ILogger<NotificationRepository> logger, NotificationsContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<NotificationEntity> GetAsync(Expression<Func<NotificationEntity, bool>> predicate)
    {
        try
        {
            var result = await _dbContext.Notifications.FirstOrDefaultAsync(predicate);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<NotificationEntity>> GetAllAsync()
    {
        try
        {
            var result = await _dbContext.Notifications.ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<NotificationEntity>> GetAllAsync(Expression<Func<NotificationEntity, bool>> predicate)
    {
        try
        {
            var result = await _dbContext.Notifications.Where(predicate).ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddAsync(NotificationEntity entity)
    {
        try
        {
            await _dbContext.Notifications.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateAsync(NotificationEntity entity)
    {
        try
        {
            var exists = await _dbContext.Notifications.FindAsync(entity);
            if (exists != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}