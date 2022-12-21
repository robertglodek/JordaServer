using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Configuration;
using Jorda.Server.Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace Jorda.Infrastructure.Services;

public class SendGridEmailService : IEmailService
{
    private readonly IOptions<EmailSettings> _options;
    private readonly ILogger<SendGridEmailService> _logger;

    public SendGridEmailService(IOptions<EmailSettings> options,ILogger<SendGridEmailService> logger)
    { 
        _options = options;
        _logger = logger;
    }
    public async Task SendEmailAsync(EmailRequest request)
    {
        var client = new SendGridClient(_options.Value.ApiKey);
        var from = new EmailAddress(_options.Value.FromAddress, _options.Value.FromName);
        var to = new EmailAddress(request.To, "End User");
        var msg = MailHelper.CreateSingleEmail(from, to, request.Subject, "", request.Body);
        var result = await client.SendEmailAsync(msg);
        if (!result.IsSuccessStatusCode)
            throw new SmtpException("Email was not sent, something went wrong");
    }
}

