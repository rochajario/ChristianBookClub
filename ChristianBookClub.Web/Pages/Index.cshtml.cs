using ChristianBookClub.Data.Entities;
using ChristianBookClub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookClub.Pages
{
    public class IndexModel(ILogger<IndexModel> logger) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;

        public string EmailPattern { get { return @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$"; } }
        public void OnGet()
        {
        }
    }
}
