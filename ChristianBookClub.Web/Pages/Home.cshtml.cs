using ChristianBookClub.Data.Entities;
using ChristianBookClub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookClub.Pages
{
    public class HomeModel(ILogger<HomeModel> logger, ISeminarService seminarService) : PageModel
    {
        private readonly ILogger<HomeModel> _logger = logger;
        private readonly ISeminarService _seminarService = seminarService;

        public IEnumerable<PublicUpcomingSeminar> UpcomingSeminars { get; private set; } = Enumerable.Empty<PublicUpcomingSeminar>();

        public void OnGet()
        {
            UpcomingSeminars = _seminarService.GetPublicUpcomingSeminars();
        }
    }
}
