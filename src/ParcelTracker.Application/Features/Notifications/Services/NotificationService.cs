using AutoMapper;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Core.Notifications;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application.Features.Notifications.Services;

public class NotificationService : INotificationService
{
    private readonly IAsyncRepository<NotificationEntity> _repository;
    private readonly IMapper _mapper;

    public NotificationService(IAsyncRepository<NotificationEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Core.Notifications.Notification>> GetAllNotifications()
    {
        var result = await _repository.GetAllAsync();
        var mappedResult = result.Select(n => _mapper.Map<Core.Notifications.Notification>(n));
        return mappedResult;
    }

    public async Task<IEnumerable<Core.Notifications.Notification>> GetAllByClientId(int clientId)
    {
        var result = await _repository.GetAllAsync(n => n.ClientId == clientId);
        var mappedResult = result.Select(n => _mapper.Map<Core.Notifications.Notification>(n));
        return mappedResult;
    }

    public async Task<Core.Notifications.Notification> CreateNotification(Core.Notifications.Notification notification)
    {
        var entity = _mapper.Map<NotificationEntity>(notification);
        await _repository.AddAsync(entity);
        return _mapper.Map<Core.Notifications.Notification>(notification);
    }

    public async Task<Core.Notifications.Notification> CreateDelivery(int clientId, string referenceId)
    {
        var notification = new Core.Notifications.Notification()
        {
            NotificationType = NotificationType.Delivery,
            ReferenceId = referenceId,
            ClientId = clientId
        };
        return await CreateNotification(notification);
    }

    public async Task<Core.Notifications.Notification> CreatePickup(int clientId, string referenceId)
    {
        var notification = new Core.Notifications.Notification()
        {
            NotificationType = NotificationType.Pickup,
            ReferenceId = referenceId,
            ClientId = clientId
        };
        return await CreateNotification(notification);
    }

    public async Task<Core.Notifications.Notification> CreateReminder(int clientId, string referenceId)
    {
        var notification = new Core.Notifications.Notification()
        {
            NotificationType = NotificationType.Reminder,
            ReferenceId = referenceId,
            ClientId = clientId
        };
        return await CreateNotification(notification);
    }
}