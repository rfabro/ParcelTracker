using AutoMapper;
using Microsoft.Extensions.Logging;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Core.Email;
using ParcelTracker.Core.Rules;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application.Features.Emails.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly ISendGridService _sendGridService;
    private readonly IAsyncRepository<RuleEntity> _repository;
    private readonly IMapper _mapper;
    private const string ModuleName = nameof(EmailService);

    public EmailService(ILogger<EmailService> logger, ISendGridService sendGridService, IAsyncRepository<RuleEntity> repository, IMapper mapper)
    {
        _logger = logger;
        _sendGridService = sendGridService;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Email> CreateEmail(int clientId, string subject, string body)
    {
        var ruleFromDb = await _repository.GetAsync(rule => rule.ClientId == clientId);
        if (ruleFromDb == null)
        {
            _logger.LogError($"{ModuleName}: CreateEmail: rule for {clientId} does not exists");
            return null;
        }

        var rule = _mapper.Map<Rule>(ruleFromDb);
        var email = new Email()
        {
            From = rule.DefaultEmailFrom,
            To = rule.DefaultEmailTo,
            Subject = subject,
            Body = body
        };

        _logger.LogInformation($"{ModuleName}: Rule is {rule.RuleName}");

        // will send the email only on weekdays aka rule 2
        if (rule.ClientId == clientId && rule.RuleName == "SendRule2")
        {
            var isWeekdayWithinRange = DateTime.UtcNow.DayOfWeek != DayOfWeek.Sunday && DateTime.UtcNow.DayOfWeek != DayOfWeek.Saturday;
            var isWeekdayHourWithinRange = DateTime.UtcNow.Hour >= 8 && DateTime.UtcNow.Hour <= 20;

            if (isWeekdayWithinRange && isWeekdayHourWithinRange)
                await SendEmail(email);

            var isSaturday = DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday;
            var isSaturdayHourWithinRange = DateTime.UtcNow.Hour >= 8 && DateTime.UtcNow.Hour <= 12;

            if (isSaturday && isSaturdayHourWithinRange)
                await SendEmail(email);
        }
        else
        {
            await SendEmail(email);
        }

        return email;
    }

    public async Task<Core.Email.Email> SendEmail(Core.Email.Email email)
    {
        _logger.LogInformation($"{ModuleName}: Email is sent as of {DateTime.UtcNow.ToLongDateString()}");
        await _sendGridService.SendEmail(email);
        return email;
    }
}