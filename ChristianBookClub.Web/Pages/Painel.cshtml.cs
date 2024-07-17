using ChristianBookClub.Data.Entities;
using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Models;
using ChristianBookClub.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookClub.Pages
{
	public class PainelModel(ISeminarService seminarService, SignInManager<User> signInManager) : PageModel
	{
		private readonly ISeminarService _seminarService = seminarService;
		private readonly SignInManager<User> _signInManager = signInManager;
		public NextSeminarRoomViewModel? NextSeminarRoom { get; private set; } = null;
		public IList<SubscriptionViewModel> SeminarDetails { get; private set; } = new List<SubscriptionViewModel>();


		public IActionResult OnGetAttendMeeting(long scheduleId)
		{
			var userId = Convert.ToInt64(_signInManager.UserManager.GetUserId(User));
			var seminar = _seminarService.GetRegisteredUpcomingSeminars(userId).First(x => x.ScheduleId.Equals(scheduleId));

			_seminarService.AddPresence(scheduleId, seminar.RegisterId);
			return Redirect($"https://meet.jit.si/fe-entre-linhas/{seminar.RoomId}");
		}

		public IActionResult OnGet()
		{
			if (!_signInManager.IsSignedIn(User))
			{
				return new RedirectToPageResult("./Index");
			}

			var userId = _signInManager.UserManager.GetUserId(User);
			LoadSubscriptions(Convert.ToInt64(userId));
			return Page();

		}

		public IActionResult OnGetSubscribe(long seminarId)
		{
			var userId = _signInManager.UserManager.GetUserId(User);
			_seminarService.AddSubscription(Convert.ToInt64(userId), seminarId);

			return RedirectToPage();
		}

		public IActionResult OnGetUnsubscribe(long seminarId)
		{
			var userId = _signInManager.UserManager.GetUserId(User);
			_seminarService.RemoveSubscription(Convert.ToInt64(userId), seminarId);

			return RedirectToPage();
		}

		private void LoadSubscriptions(long userId)
		{
			NextSeminarRoom = null;
			SeminarDetails = Enumerable.Empty<SubscriptionViewModel>().ToList();

			var subscriptions = _seminarService.GetSubscsriptions(userId).ToList();
			var registeredSeminars = _seminarService.GetRegisteredUpcomingSeminars(userId);

			_seminarService.GetPublicUpcomingSeminars().ToList().ForEach(x =>
			{
				SeminarDetails.Add(new SubscriptionViewModel
				{
					SeminarId = x.SeminarId,
					Details = x.Details,
					Name = x.Name,
					NextMeeting = x.NextMeeting,
					Registered = registeredSeminars.Any(i => i.SeminarId.Equals(x.SeminarId))
				});
			});

			if (registeredSeminars.Any())
			{
				var nextSeminar = registeredSeminars.FirstOrDefault(x => DateTime.Now <= x.NextMeeting.AddHours(1));
				if (nextSeminar is not null)
				{
					NextSeminarRoom = new NextSeminarRoomViewModel(nextSeminar);
					return;
				}
				NextSeminarRoom = null;
			}
		}
	}

}
