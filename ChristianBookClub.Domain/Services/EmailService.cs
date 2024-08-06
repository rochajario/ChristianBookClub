using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Models;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChristianBookClub.Domain.Services
{
    public class EmailService : IEmailService
    {
        public readonly ILogger<EmailService> _logger;
        private readonly IFluentEmailFactory _fluentEmailFactory;
        private readonly string? _author;
        private readonly string? _fromEmail;

        public EmailService(ILogger<EmailService> logger, IFluentEmailFactory fluentEmailFactory, IConfiguration configuration)
        {
            _logger = logger;
            _fluentEmailFactory = fluentEmailFactory;

            _author = configuration.GetSection("EmailSettings")["Author"];
            _fromEmail = configuration.GetSection("EmailSettings")["Username"];
        }

        public async Task Send(EmailMessageModel emailMessageModel)
        {
            _logger.LogInformation($"Sending Email to: {emailMessageModel.ToAddress}");
            var from = String.IsNullOrEmpty(_fromEmail) ? "contato@feentrelinhas.com" : _fromEmail;
            var author = String.IsNullOrEmpty(_author) ? "Contato - Fé entre Linhas" : _author;

            await _fluentEmailFactory
                .Create()
                .To(emailMessageModel.ToAddress, emailMessageModel.ToAddress)
                .SetFrom(from, author)
                .Subject(emailMessageModel.Subject)
                .Body(emailMessageModel.Body, true)
                .SendAsync();
        }

    }
}
