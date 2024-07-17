using ChristianBookClub.Data.Entities;
using ChristianBookClub.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristianBookClub.Data.Repositories
{
	public class SeminarRepository(ApplicationDbContext context) : BaseRepository<Seminar>(context), ISeminarRepository
	{

		public IEnumerable<PublicUpcomingSeminar> GetPublicUpcomingSeminars()
		{
			return _context.PublicUpcomingSeminars.AsNoTracking();
		}

		public void AddSubscription(long userId, long seminarId)
		{
			_context.SeminarRegisters.Add(new SeminarRegister { UserId = userId, SeminarId = seminarId });
			_context.SaveChanges();
		}

		public void RemoveSubscription(long userId, long seminarId)
		{
			var item = _context.SeminarRegisters.Single(x => x.UserId.Equals(userId) && x.SeminarId.Equals(seminarId));
			if (item is not null)
			{
				_context.SeminarRegisters.Remove(item);
				_context.SaveChanges();
			}
		}

		public IEnumerable<RegisteredUpcomingSeminar> GetRegisteredUpcomingSeminars()
		{
			return _context.RegisteredUpcomingSeminars.AsNoTracking();
		}

		public void AddPresence(long scheduleId, long registerId)
		{
			if (!_context.SeminarAttendances.Any(x => x.SeminarRegisterId.Equals(registerId) && x.SeminarScheduleId.Equals(scheduleId)))
			{
				_context.SeminarAttendances.Add(new SeminarAttendance { SeminarRegisterId = registerId, SeminarScheduleId = scheduleId });
				_context.SaveChanges();
			}
		}

		public IEnumerable<UserHistoric> GetUserHistorics()
		{
			return _context.UsersHistoric.AsNoTracking();
		}
	}
}
