using ChristianBookClub.Data.Entities;
using ChristianBookClub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookClub.Pages
{
    public class IndexModel(ILogger<IndexModel> logger) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;


        public void OnGet()
        {
        }
    }
}
