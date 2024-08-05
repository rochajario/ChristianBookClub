using FluentEmail.Core.Interfaces;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

namespace ChristianBookClub.Domain.Extensions
{
	public static class FluentEmailExtensions
	{
		public static IServiceCollection AddFluentEMail(this IServiceCollection services, IConfigurationManager configuration)
		{
			var emailSettings = configuration.GetSection("EmailSettings");
			var defaultFromEmail = emailSettings["DefaultFromEmail"];
			var host = emailSettings["Host"];
			var port = Convert.ToInt32(emailSettings["Port"]);

			var client = new SmtpClient(host, port)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"])
			};

			services.AddFluentEmail(defaultFromEmail);
			services.AddSingleton<ISender>(x => new SmtpSender(client));
			return services;
		}
	}
}
