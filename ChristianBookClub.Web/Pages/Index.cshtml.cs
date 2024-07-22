using ChristianBookClub.Data.Entities;
using ChristianBookClub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookClub.Pages
{
	public class IndexModel(ISeminarService seminarService) : PageModel
	{
		private readonly ISeminarService _seminarService = seminarService;

		public IEnumerable<PublicUpcomingSeminar> UpcomingSeminars { get; private set; } = Enumerable.Empty<PublicUpcomingSeminar>();

		public void OnGet()
		{
			UpcomingSeminars = _seminarService.GetPublicUpcomingSeminars();
		}
	}
}
