using ChristianBookClub.Data.Entities;

namespace ChristianBookClub.Data.Interfaces
{
    public interface ISeminarRepository : IRepository<Seminar>
    {
        void AddSubscription(long userId, long seminarId);
        IEnumerable<PublicUpcomingSeminar> GetPublicUpcomingSeminars();
        void RemoveSubscription(long userId, long seminarId);
		IEnumerable<RegisteredUpcomingSeminar> GetRegisteredUpcomingSeminars();
		void AddPresence(long scheduleId, long registerId);
		IEnumerable<UserHistoric> GetUserHistorics();
	}
}
