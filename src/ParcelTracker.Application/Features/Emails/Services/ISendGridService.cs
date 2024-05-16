namespace ParcelTracker.Application.Features.Emails.Services;

public interface ISendGridService
{
    Task<Core.Email.Email> SendEmail(Core.Email.Email email);
}