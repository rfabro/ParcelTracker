using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParcelTracker.Application.Features.Rules.Models;
using ParcelTracker.Application.Features.Rules.Services;
using ParcelTracker.Common.Helpers;
using ParcelTracker.Common.Models;
using ParcelTracker.Core.Rules;
using Swashbuckle.AspNetCore.Annotations;

namespace ParcelTracker.API.Controllers.Rules;

[ApiController]
[Route("api/[controller]")]
public class RulesController : ControllerBase
{
    private const string ControllerTagName = "Rules";
    private const string ModuleName = nameof(RulesController);

    private readonly ILogger<RulesController> _logger;
    private readonly IMapper _mapper;
    private readonly IRuleService _rulesService;

    public RulesController(ILogger<RulesController> logger, IMapper mapper, IRuleService rulesService)
    {
        _logger = logger;
        _mapper = mapper;
        _rulesService = rulesService;
    }

    /// <summary>
    /// Get all rules
    /// </summary>
    /// <returns>List of Rules</returns>
    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(BaseResult<RuleModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<RuleModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseResult<RuleModel>> GetAll()
    {
        try
        {
            _logger.LogDebug($"{ModuleName}: Request: GetAllRules");
            var getResult = await _rulesService.GetAllRules();
            var parsedResult = getResult.Select(n => _mapper.Map<RuleModel>(n));
            _logger.LogDebug($"{ModuleName}: Response: GetAllRules success");
            return new BaseResult<RuleModel>(parsedResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on GetAllRules. {ex?.Message ?? ex.InnerException?.Message}");
            return new BaseResult<RuleModel>(ex.Message);
        }
    }

    /// <summary>
    /// Create New Rule
    /// </summary>
    /// <param name="modelBody">RuleRequest</param>
    /// <returns>Created Rule</returns>
    [HttpPut("Rule", Name = "Create New Rule")]
    [ProducesResponseType(typeof(BaseResult<RuleModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<RuleModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseResult<RuleModel>> CreateRule([FromBody]RuleRequest modelBody)
    {
        try
        {
            _logger.LogDebug($"{ModuleName}: Request: CreateRule");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"{ModuleName}: Error on CreateRule. {ValidationHelper.GetValidationErrors(ModelState)}");
                return new BaseResult<RuleModel>(ValidationHelper.GetValidationErrors(ModelState));
            }
            var rule = _mapper.Map<Rule>(modelBody);
            var createResult = await _rulesService.CreateRule(rule);
            var parsedResult = _mapper.Map<RuleModel>(createResult);
            return new BaseResult<RuleModel>(parsedResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on CreateRule. {ex?.Message ?? ex.InnerException?.Message}");
            return new BaseResult<RuleModel>(ex.Message);
        }
    }
}