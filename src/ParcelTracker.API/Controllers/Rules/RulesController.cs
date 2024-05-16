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
    [HttpGet("GetAllRules", Name = "Get All Rules")]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(BaseResult<RuleModel>), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResult<RuleModel>), (int) HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Tags = new[] { ControllerTagName })]
    public async Task<BaseResult<RuleModel>> GetAllRules()
    {
        try
        {
            var getResult = await _rulesService.GetAllRules();
            var parsedResult = getResult.Select(n => _mapper.Map<RuleModel>(n));
            return new BaseResult<RuleModel>(parsedResult);
        }
        catch (Exception ex)
        {
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
            if (!ModelState.IsValid)
                return new BaseResult<RuleModel>(ValidationHelper.GetValidationErrors(ModelState));
            var rule = _mapper.Map<Rule>(modelBody);
            var createResult = await _rulesService.CreateRule(rule);
            var parsedResult = _mapper.Map<RuleModel>(createResult);
            return new BaseResult<RuleModel>(parsedResult);
        }
        catch (Exception ex)
        {
            return new BaseResult<RuleModel>(ex.Message);
        }
    }
}