using System.Security.Cryptography;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParcelTracker.Application.Abstractions;
using ParcelTracker.Application.Features.Emails.Options;
using ParcelTracker.Infrastructure.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ParcelTracker.Application.Features.Emails.Services;

public class SendGridService : ISendGridService
{
    private readonly ILogger<SendGridService> _logger;
    private readonly IAsyncRepository<NotificationEntity> _repository;
    private readonly SendGridOptions _options;
    private const string ModuleName = nameof(SendGridService);

    public SendGridService(ILogger<SendGridService> logger, IAsyncRepository<NotificationEntity> repository, IOptions<SendGridOptions> options)
    {
        _logger = logger;
        _repository = repository;
        _options = options.Value;
    }

    public async Task<Core.Email.Email> SendEmail(Core.Email.Email email)
    {
        var apikey = _options.SendGridApiKey;
        var client = new SendGridClient(apikey);

        var from = new EmailAddress(email.From);
        var to = new EmailAddress(email.To);
        var htmlContent = "";
        var textContent = ($" Sending Email from {from.Email} to {to.Email}. {email.Body}");
        try
        {
            var message = await Task.Run(() =>
                MailHelper.CreateSingleEmail(from, to, email.Subject, textContent, htmlContent));
            var response = await client.SendEmailAsync(message);

            _logger.LogInformation($"{ModuleName}: SendEmail: SendGridResponse: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ModuleName}: Error on SendEmail. {ex?.Message ?? ex.InnerException?.Message}");
            throw;
        }

        return email;
    }
}