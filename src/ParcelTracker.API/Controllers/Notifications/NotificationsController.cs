using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParcelTracker.Application.Features.Notification.Services;
using ParcelTracker.Application.Features.Notifications.Models;
using ParcelTracker.Application.Features.Notifications.Services;
using ParcelTracker.Common.Helpers;
using ParcelTracker.Common.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ParcelTracker.API.Controllers.Notifications;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private const string ControllerTagName = "Notifications";

    private readonly ILogger<NotificationsController> _logger;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public NotificationsController(ILogger<NotificationsController> logger, IMapper mapper, INotificationService notificationService)
    {
        _logger = logger;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Get all notifications
    /// </summary>
    /// <returns>List of notifications</returns>
    [HttpGet("GetAllNotifications", Name = "GetAllNotifications")]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseResult<NotificationModel>> GetAllNotifications()
    {
        try
        {
            var getResult = await _notificationService.GetAllNotifications();
            var parsedResult = getResult.Select(n => _mapper.Map<NotificationModel>(n));
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            return new BaseResult<NotificationModel>(ex.Message);
        }
    }

    /// <summary>
    /// Get all notifications by Client Id
    /// </summary>
    /// <param name="clientId">The client Id to query</param>
    /// <returns>List of notifications</returns>
    [HttpGet("GetAllByClientId", Name = "GetAllByClientId")]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseResult<NotificationModel>> GetAllNotificationsByClientId([FromQuery]int clientId)
    {
        try
        {
            if (!ModelState.IsValid)
                return new BaseResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));

            var getResult = await _notificationService.GetAllByClientId(clientId);
            var parsedResult = getResult.Select(n => _mapper.Map<NotificationModel>(n));
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            return new BaseResult<NotificationModel>(ex.Message);
        }
    }

    /// <summary>
    /// Create New Delivery
    /// </summary>
    /// <param name="modelBody">Request body that contains the clientid and referenceId</param>
    /// <returns>Created Delivery Notification</returns>
    [HttpPut("Delivery", Name = "Create New Delivery")]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { "Delivery" })]
    public async Task<BaseResult<NotificationModel>> CreateNewDelivery([FromBody]NotificationRequest modelBody)
    {
        try
        {
            if (!ModelState.IsValid)
                return new BaseResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));

            var createResult = await _notificationService.CreateDelivery(modelBody.ClientId,
                modelBody.ReferenceId);
            var parsedResult = _mapper.Map<NotificationModel>(createResult);
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            return new BaseResult<NotificationModel>(ex.Message);
        }
    }

    /// <summary>
    /// Create New Pickup
    /// </summary>
    /// <param name="modelBody">Request body that contains the clientid and referenceId</param>
    /// <returns>Created Pickup Notification</returns>
    [HttpPut("Pickup", Name = "Create New Pickup")]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { "Pickup" })]
    public async Task<BaseResult<NotificationModel>> CreateNewPickup([FromBody]NotificationRequest modelBody)
    {
        try
        {
            if (!ModelState.IsValid)
                return new BaseResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));

            var createResult = await _notificationService.CreatePickup(modelBody.ClientId,
                modelBody.ReferenceId);
            var parsedResult = _mapper.Map<NotificationModel>(createResult);
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            return new BaseResult<NotificationModel>(ex.Message);
        }
    }

    /// <summary>
    /// Create New Reminder
    /// </summary>
    /// <param name="modelBody">Request body that contains the clientid and referenceId</param>
    /// <returns>Created Reminder Notification</returns>
    [HttpPut("Reminder", Name = "Create New Reminder")]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { "Reminder" })]
    public async Task<BaseResult<NotificationModel>> CreateNewReminder([FromBody]NotificationRequest modelBody)
    {
        try
        {
            if (!ModelState.IsValid)
                return new BaseResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));

            var createResult = await _notificationService.CreateReminder(modelBody.ClientId,
                modelBody.ReferenceId);
            var parsedResult = _mapper.Map<NotificationModel>(createResult);
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            return new BaseResult<NotificationModel>(ex.Message);
        }
    }
}