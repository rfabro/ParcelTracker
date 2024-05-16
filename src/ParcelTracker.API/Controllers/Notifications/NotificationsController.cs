using System.Net;
using Microsoft.AspNetCore.Mvc;
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
    private readonly INotificationService _notificationService;

    public NotificationsController(ILogger<NotificationsController> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Get all notifications by Client Id
    /// </summary>
    /// <param name="clientId">The client Id to query</param>
    /// <returns>List of notifications</returns>
    [HttpGet("GetAllNotificationsByClientId", Name = "GetAllNotificationsByClientId")]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(BaseDomainResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseDomainResult<NotificationModel>> GetAllNotificationsByClientId([FromQuery]string clientId)
    {
        try
        {
            if (!ModelState.IsValid)
                return new BaseDomainResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));

            var getResult = await _notificationService.GetAllByClientId(clientId, true);
            return new BaseDomainResult<NotificationModel>(getResult);
        }
        catch (Exception ex)
        {
            return new BaseDomainResult<NotificationModel>(ex.Message);
        }
    }

    /// <summary>
    /// Create New Delivery
    /// </summary>
    /// <param name="modelBody">Request body that contains the clientid and referenceId</param>
    /// <returns>Created Notification</returns>
    [HttpPost("CreateNewDelivery", Name = "Create New Delivery")]
    [ProducesResponseType(typeof(BaseDomainResult<NotificationModel>), (int) HttpStatusCode.OK)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseDomainResult<NotificationModel>> CreateNewDelivery([FromBody]NotificationModel modelBody)
    {
        try
        {
            if (!ModelState.IsValid)
                return new BaseDomainResult<NotificationModel>(ValidationHelper.GetValidationErrors(ModelState));

            var createResult = await _notificationService.CreateDelivery(modelBody.ClientId,
                modelBody.ReferenceId);

            return new BaseDomainResult<NotificationModel>(createResult);
        }
        catch (Exception ex)
        {
            return new BaseDomainResult<NotificationModel>(ex.Message);
        }
    }
}