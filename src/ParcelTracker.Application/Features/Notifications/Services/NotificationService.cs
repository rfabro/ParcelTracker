using AutoMapper;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Application.Features.Notifications.Models;
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

    public async Task<IEnumerable<NotificationModel>> GetAllByClientId(string clientId, bool includesDeleted)
    {
        // return new List<NotificationDto>()
        // {
        //     new NotificationDto() { ClientId = 1 },
        //     new NotificationDto() { ClientId = 2 }
        // };

        var result = await _repository.GetAllAsync();
        var mappedResult = result.Select(n => _mapper.Map<NotificationModel>(n));
        return mappedResult;
    }

    public async Task<NotificationModel> CreateNotification(Notification notification)
    {
        var entity = _mapper.Map<NotificationEntity>(notification);
        await _repository.AddAsync(entity);
        return _mapper.Map<NotificationModel>(notification);
    }

    public async Task<NotificationModel> CreateDelivery(int clientId, string referenceId)
    {
        var notification = new Notification()
        {
            NotificationType = NotificationType.Delivery,
            ReferenceId = referenceId,
            ClientId = clientId
        };
        return await CreateNotification(notification);
    }

    public async Task<NotificationModel> CreatePickup(int clientId, string referenceId)
    {
        var notification = new Notification()
        {
            NotificationType = NotificationType.Pickup,
            ReferenceId = referenceId,
            ClientId = clientId
        };
        return await CreateNotification(notification);
    }

    public async Task<NotificationModel> CreateReminder(int clientId, string referenceId)
    {
        var notification = new Notification()
        {
            NotificationType = NotificationType.Reminder,
            ReferenceId = referenceId,
            ClientId = clientId
        };
        return await CreateNotification(notification);
    }
}