using ChristianBookClub.Domain.Models;

namespace ChristianBookClub.Domain.Interfaces
{
	public interface IEmailService
	{
		Task Send(EmailMessageModel emailMessageModel);
	}
}
