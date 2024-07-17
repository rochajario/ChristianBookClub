using ChristianBookClub.Data.Entities;

namespace ChristianBookClub.Domain.Interfaces
{
	public interface ICertificateService
	{
		Dictionary<long, string> GetCertificates(long userId);
	}
}
