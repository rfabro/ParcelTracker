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
    private const string ModuleName = nameof(NotificationsController);

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
    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseResult<NotificationModel>> GetAll()
    {
        try
        {
            _logger.LogDebug($"{ModuleName}: Request: GetAllNotifications");
            var getResult = await _notificationService.GetAllNotifications();
            var parsedResult = getResult.Select(n => _mapper.Map<NotificationModel>(n));
            _logger.LogDebug($"{ModuleName}: Response: GetAllNotifications success");
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on GetAllNotifications. {ex?.Message ?? ex.InnerException?.Message}");
            return new BaseResult<NotificationModel>(ex.Message);
        }
    }

    /// <summary>
    /// Get all notifications by Client Id
    /// </summary>
    /// <param name="clientId">The client Id to query</param>
    /// <returns>List of notifications</returns>
    [HttpGet("{clientId}")]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<NotificationModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseResult<NotificationModel>> GetAllByClientId([FromQuery]int clientId)
    {
        try
        {
            _logger.LogDebug($"{ModuleName}: Request: GetAllByClientId");

            if (!ModelState.IsValid)
            {
                _logger.LogError($"{ModuleName}: Error on GetAllByClientId. {ValidationHelper.GetValidationErrors(ModelState)}");
                return new BaseResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));
            }

            var getResult = await _notificationService.GetAllByClientId(clientId);
            var parsedResult = getResult.Select(n => _mapper.Map<NotificationModel>(n));
            _logger.LogDebug($"{ModuleName}: Response: GetAllByClientId success");
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on GetAllByClientId. {ex?.Message ?? ex.InnerException?.Message}");
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
            _logger.LogDebug($"{ModuleName}: Request: CreateNewDelivery");

            if (!ModelState.IsValid)
            {
                _logger.LogError($"{ModuleName}: Error on CreateNewDelivery. {ValidationHelper.GetValidationErrors(ModelState)}");
                return new BaseResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));
            }

            var createResult = await _notificationService.CreateDelivery(modelBody.ClientId,
                modelBody.ReferenceId);
            var parsedResult = _mapper.Map<NotificationModel>(createResult);

            _logger.LogDebug($"{ModuleName}: Response: CreateNewDelivery success");
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on CreateNewDelivery. {ex?.Message ?? ex.InnerException?.Message}");
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
            _logger.LogDebug($"{ModuleName}: Request: CreateNewPickup");

            if (!ModelState.IsValid)
            {
                _logger.LogError($"{ModuleName}: Error on CreateNewPickup. {ValidationHelper.GetValidationErrors(ModelState)}");
                return new BaseResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));
            }

            var createResult = await _notificationService.CreatePickup(modelBody.ClientId,
                modelBody.ReferenceId);
            var parsedResult = _mapper.Map<NotificationModel>(createResult);
            _logger.LogDebug($"{ModuleName}: Response: CreateNewPickup success");
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on CreateNewPickup. {ex?.Message ?? ex.InnerException?.Message}");
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
            _logger.LogDebug($"{ModuleName}: Request: CreateNewReminder");

            if (!ModelState.IsValid)
            {
                _logger.LogError($"{ModuleName}: Error on CreateNewReminder. {ValidationHelper.GetValidationErrors(ModelState)}");
                return new BaseResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));
            }

            var createResult = await _notificationService.CreateReminder(modelBody.ClientId,
                modelBody.ReferenceId);
            var parsedResult = _mapper.Map<NotificationModel>(createResult);

            _logger.LogDebug($"{ModuleName}: Response: CreateNewReminder success");
            return new BaseResult<NotificationModel>(parsedResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on CreateNewReminder. {ex?.Message ?? ex.InnerException?.Message}");
            return new BaseResult<NotificationModel>(ex.Message);
        }
    }
}