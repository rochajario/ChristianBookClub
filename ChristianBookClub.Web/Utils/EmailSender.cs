using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ChristianBookClub.Web.Utils
{
	public class EmailSender(IEmailService emailService) : IEmailSender
	{
		IEmailService _emailService = emailService;
		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			EmailMessageModel message = new EmailMessageModel()
			{
				ToAddress = email,
				Subject = subject,
				Body = htmlMessage
			};

			await _emailService.Send(message);
		}
	}
}
