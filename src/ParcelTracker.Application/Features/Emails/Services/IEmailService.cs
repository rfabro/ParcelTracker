namespace ParcelTracker.Application.Features.Emails.Services;

public interface IEmailService
{
    Task<Core.Email.Email> CreateEmail(int clientId, string subject, string body);
    Task<Core.Email.Email> SendEmail(Core.Email.Email email);
}