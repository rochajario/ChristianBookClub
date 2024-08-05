using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Models;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;

namespace ChristianBookClub.Domain.Services
{
    public class EmailService : IEmailService
    {
        public readonly ILogger<EmailService> _logger;
        private readonly IFluentEmailFactory _fluentEmailFactory;

        public EmailService(ILogger<EmailService> logger, IFluentEmailFactory fluentEmailFactory)
        {
            _logger = logger;
            _fluentEmailFactory = fluentEmailFactory;
        }

        public async Task Send(EmailMessageModel emailMessageModel)
        {
            _logger.LogInformation("Sending Email");
            await _fluentEmailFactory
                .Create()
                .To(emailMessageModel.ToAddress, "")
                .Subject(emailMessageModel.Subject)
                .Body(emailMessageModel.Body, true)
                .SendAsync();
        }

    }
}
