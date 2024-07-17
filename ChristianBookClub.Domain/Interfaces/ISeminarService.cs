using ChristianBookClub.Data.Entities;
using ChristianBookClub.Domain.Models;
using ChristianBookClub.Domain.Models.Forms;

namespace ChristianBookClub.Domain.Interfaces
{
    public interface ISeminarService
    {
        void AddSubscription(long userId, long seminarId);
        public void Create(SeminarForm form);
		Seminar GetSeminar(long id);
		IEnumerable<Seminar> GetSeminars(Func<Seminar, bool> function);
        IEnumerable<SubscriptionDetails> GetSubscsriptions(long userId);
        IEnumerable<PublicUpcomingSeminar> GetPublicUpcomingSeminars();
        void RemoveSubscription(long userId, long seminarId);
		IEnumerable<RegisteredUpcomingSeminar> GetRegisteredUpcomingSeminars(long userId);
		void AddPresence(long scheduleId, long registerId);
		IList<UserHistoric> GetUserHistoric(long userId);
	}
}
