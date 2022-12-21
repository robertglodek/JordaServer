using Jorda.Server.Application.Common.Models;

namespace Jorda.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequest request);
}
