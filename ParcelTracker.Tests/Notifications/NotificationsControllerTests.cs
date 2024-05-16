using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using ParcelTracker.API.Controllers.Notifications;
using ParcelTracker.Application.Features.Notifications.Models;
using ParcelTracker.Application.Features.Notifications.Services;
using ParcelTracker.Core.Notifications;

namespace ParcelTracker.Tests.Notifications;

[TestClass]
public class NotificationsControllerTests
{
    private NotificationsController _controller;
    private List<string> _logMessages;
    private Mock<IMapper> _mapper;
    private Mock<ILogger<NotificationsController>> _logger;
    private Mock<INotificationService> _notificationService;
    private Notification _notification;
    private NotificationModel _notificationModel;

    private int _clientId;
    private string _referenceId;
    private NotificationType _notificationType;

    [TestInitialize]
    public void Initialize()
    {
        _logMessages = new();
        _mapper = new();
        _logger = Extensions.LoggerExtensions.BuildMockLogger<NotificationsController>(_logMessages);
        _notificationService = new();
        _clientId = 1;
        _referenceId = "sample reference";
        _notificationType = NotificationType.Delivery;
        _notification = new Notification()
        {
            ClientId = _clientId,
            ReferenceId = _referenceId,
            NotificationType = _notificationType
        };
        _notificationModel = new NotificationModel()
        {
            ClientId = _clientId,
            ReferenceId = _referenceId,
            NotificationType = _notificationType
        };

        _notificationService
            .Setup(s => s.CreateNotification(It.IsAny<Notification>()))
            .Returns(Task.FromResult(_notification))
            .Verifiable();

        _notificationService
            .Setup(s => s.CreateDelivery(It.IsAny<int>(), It.IsAny<string>()))
            .Returns(Task.FromResult(_notification))
            .Verifiable();

        _notificationService
            .Setup(s => s.CreateNewPickup(It.IsAny<int>(), It.IsAny<string>()))
            .Returns(Task.FromResult(_notification))
            .Verifiable();

        _notificationService
            .Setup(s => s.CreateReminder(It.IsAny<int>(), It.IsAny<string>()))
            .Returns(Task.FromResult(_notification))
            .Verifiable();

        _mapper
            .Setup(s => s.Map<NotificationModel>(It.IsAny<Notification>()))
            .Returns(_notificationModel)
            .Verifiable();

        _controller = new NotificationsController(_logger.Object, _mapper.Object, _notificationService.Object);
    }

    [TestMethod]
    public async Task CreateNewDelivery_Returns_Notification_Success()
    {
        // Arrange
        var notificationRequest = new NotificationRequest()
        {
            ClientId = _clientId,
            ReferenceId = _referenceId
        };

        // Act
        var result = await _controller.CreateNewDelivery(notificationRequest);

        // Assert
        _notificationService.Verify(s => s.CreateDelivery(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        _mapper.Verify(s => s.Map<NotificationModel>(It.IsAny<Notification>()), Times.Once);
        Assert.AreEqual(2, _logMessages.Count);
    }

    [TestMethod]
    public async Task CreateNewDelivery_ValidationFail()
    {
        // Arrange
        var notificationRequest = new NotificationRequest()
        {
            ClientId = _clientId,
            ReferenceId = _referenceId
        };

        // Act
        _controller.ModelState.AddModelError("ClientId", "ClientId has invalid value");
        _controller.ModelState.AddModelError("ReferenceId", "must have a value");
        var result = await _controller.CreateNewDelivery(notificationRequest);

        // Assert
        _notificationService.Verify(s => s.CreateDelivery(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        _mapper.Verify(s => s.Map<NotificationModel>(It.IsAny<Notification>()), Times.Never);
        Assert.AreEqual(2, _logMessages.Count);
    }

    [TestMethod]
    public async Task CreateNewPickup_Returns_Notification_Success()
    {
        // Arrange
        var notificationRequest = new NotificationRequest()
        {
            ClientId = _clientId,
            ReferenceId = _referenceId
        };

        // Act
        var result = await _controller.CreateNewPickup(notificationRequest);

        // Assert
        _notificationService.Verify(s => s.CreateNewPickup(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        _mapper.Verify(s => s.Map<NotificationModel>(It.IsAny<Notification>()), Times.Once);
        Assert.AreEqual(2, _logMessages.Count);
    }

    [TestMethod]
    public async Task CreateNewPickup_ValidationFail()
    {
        // Arrange
        var notificationRequest = new NotificationRequest()
        {
            ClientId = _clientId,
            ReferenceId = _referenceId
        };

        // Act
        _controller.ModelState.AddModelError("ClientId", "ClientId has invalid value");
        _controller.ModelState.AddModelError("ReferenceId", "must have a value");
        var result = await _controller.CreateNewPickup(notificationRequest);

        // Assert
        _notificationService.Verify(s => s.CreateNewPickup(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        _mapper.Verify(s => s.Map<NotificationModel>(It.IsAny<Notification>()), Times.Never);
        Assert.AreEqual(2, _logMessages.Count);
    }

    [TestMethod]
    public async Task CreateNewReminder_Returns_Notification_Success()
    {
        // Arrange
        var notificationRequest = new NotificationRequest()
        {
            ClientId = _clientId,
            ReferenceId = _referenceId
        };

        // Act
        var result = await _controller.CreateNewReminder(notificationRequest);

        // Assert
        _notificationService.Verify(s => s.CreateReminder(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        _mapper.Verify(s => s.Map<NotificationModel>(It.IsAny<Notification>()), Times.Once);
        Assert.AreEqual(2, _logMessages.Count);
    }

    [TestMethod]
    public async Task CreateNewReminder_ValidationFail()
    {
        // Arrange
        var notificationRequest = new NotificationRequest()
        {
            ClientId = _clientId,
            ReferenceId = _referenceId
        };

        // Act
        _controller.ModelState.AddModelError("ClientId", "ClientId has invalid value");
        _controller.ModelState.AddModelError("ReferenceId", "must have a value");
        var result = await _controller.CreateNewReminder(notificationRequest);

        // Assert
        _notificationService.Verify(s => s.CreateDelivery(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        _mapper.Verify(s => s.Map<NotificationModel>(It.IsAny<Notification>()), Times.Never);
        Assert.AreEqual(2, _logMessages.Count);
    }
}