using AutoMapper;
using Microsoft.Extensions.Logging;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Application.Features.Emails.Services;
using ParcelTracker.Core.Notifications;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application.Features.Notifications.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IAsyncRepository<NotificationEntity> _repository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private const string ModuleName = nameof(NotificationService);

    public NotificationService(
        ILogger<NotificationService> logger,
        IAsyncRepository<NotificationEntity> repository,
        IEmailService emailService,
        IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _emailService = emailService;
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

        await CreateNotification(notification);
        _logger.LogInformation($"{ModuleName}: CreateDelivery: Delivery notification created");

        var subject = $"Delivery notification {referenceId}";
        await SendEmailNotification(clientId, referenceId, subject);

        return notification;
    }

    public async Task<Core.Notifications.Notification> CreatePickup(int clientId, string referenceId)
    {
        var notification = new Core.Notifications.Notification()
        {
            NotificationType = NotificationType.Pickup,
            ReferenceId = referenceId,
            ClientId = clientId
        };
        await CreateNotification(notification);
        _logger.LogInformation($"{ModuleName}: CreatePickup: Pickup notification created");

        var subject = $"Pickup notification {referenceId}";
        await SendEmailNotification(clientId, referenceId, subject);

        return notification;
    }

    public async Task<Core.Notifications.Notification> CreateReminder(int clientId, string referenceId)
    {
        var notification = new Core.Notifications.Notification()
        {
            NotificationType = NotificationType.Reminder,
            ReferenceId = referenceId,
            ClientId = clientId
        };
        await CreateNotification(notification);
        _logger.LogInformation($"{ModuleName}: CreateReminder: Reminder notification created");

        var subject = $"Reminder notification {referenceId}";
        await SendEmailNotification(clientId, referenceId, subject);

        return notification;
    }

    private async Task SendEmailNotification(int clientId, string referenceId, string subject)
    {
        var body = $"{referenceId}";
        var email = await _emailService.CreateEmail(clientId, subject, body);
        await _emailService.SendEmail(email);
    }
}