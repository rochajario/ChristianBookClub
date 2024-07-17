using ChristianBookClub.Data.Entities;
using ChristianBookClub.Data.Interfaces;
using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Models;
using ChristianBookClub.Domain.Models.Forms;

namespace ChristianBookClub.Domain.Services
{
	public class SeminarService(ISeminarRepository seminarRepository) : ISeminarService
	{
		private readonly ISeminarRepository _seminarRepository = seminarRepository;

		public void Create(SeminarForm form)
		{
			var item = new Seminar
			{
				Name = form.Name,
				Description = form.Description,
				CoverImage = form.CoverImage
			};

			var schedules = new List<SeminarSchedule>();
			foreach (var schedule in form.Schedules)
			{
				schedules.Add(new SeminarSchedule
				{
					MeetingDate = schedule.MeetingDate,
					MeetingDetails = schedule.MeetingDetails
				});
			}

			item.SeminarSchedules = schedules;
			_seminarRepository.Create(item);
		}

		public IEnumerable<Seminar> GetSeminars(Func<Seminar, bool> function)
		{
			return _seminarRepository.GetAll().Where(function);
		}

		public Seminar GetSeminar(long id)
		{
			return _seminarRepository.GetById(id);
		}

		public IEnumerable<PublicUpcomingSeminar> GetPublicUpcomingSeminars()
		{
			return _seminarRepository.GetPublicUpcomingSeminars();
		}

		public IEnumerable<RegisteredUpcomingSeminar> GetRegisteredUpcomingSeminars(long userId)
		{
			return _seminarRepository.GetRegisteredUpcomingSeminars().Where(x => x.UserId.Equals(userId));
		}

		public void AddSubscription(long userId, long seminarId)
		{
			_seminarRepository.AddSubscription(userId, seminarId);
		}

		public void RemoveSubscription(long userId, long seminarId)
		{
			_seminarRepository.RemoveSubscription(userId, seminarId);
		}

		public void AddPresence(long scheduleId, long registerId)
		{
			_seminarRepository.AddPresence(scheduleId, registerId);
		}

		public IEnumerable<SubscriptionDetails> GetSubscsriptions(long userId)
		{
			var seminars = (
				from s in _seminarRepository.Context.Seminars
				join ss in _seminarRepository.Context.SeminarSchedules on s.Id equals ss.SeminarId
				where DateTime.Now > ss.MeetingDate
				group s by new { s.Id, s.Name } into result
				select new SubscriptionDetails
				{
					SeminarId = result.Key.Id,
					Name = result.Key.Name,
					Registered = false
				}).ToList();

			var registeredSeminars = _seminarRepository.Context.RegisteredUpcomingSeminars.Where(x => x.UserId.Equals(userId)).ToList();

			seminars.ForEach(seminar =>
			{
				if(registeredSeminars.Any(i => i.SeminarId.Equals(seminar.SeminarId)))
				{
					seminar.Registered = true;
				}
			});

			return seminars;
		}

		public IList<UserHistoric> GetUserHistoric(long userId)
		{
			return _seminarRepository.GetUserHistorics().Where(x => x.UserId.Equals(userId)).ToList();
		}
	}
}
