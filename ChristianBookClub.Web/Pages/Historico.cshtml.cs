using ChristianBookClub.Data.Entities;
using ChristianBookClub.Data.Interfaces;
using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChristianBookClub.Web.Pages
{
	public class HistoricoModel(SignInManager<User> signInManager, ISeminarService seminarService, ICertificateService certificateService, ISeminarRepository seminarRepository) : PageModel
	{
		private readonly ISeminarService _seminarService = seminarService;
		private readonly ICertificateService _certificateService = certificateService;
		private readonly SignInManager<User> _signInManager = signInManager;
		private readonly ISeminarRepository _seminarRepository = seminarRepository;

		public IList<UserHistoric> UserHistoric { get; private set; } = new List<UserHistoric>();
		public Dictionary<long, string> Certificates { get; private set; }
		public bool CertficatesEnabled { get; private set; } = false;

        public IActionResult OnGet()
		{
			if (!_signInManager.IsSignedIn(User))
			{
				return new RedirectToPageResult("./Index");
			}

			var userId = Convert.ToInt64(_signInManager.UserManager.GetUserId(User));
			CertficatesEnabled = _seminarRepository.Context.Users.Any(u => u.Id.Equals(userId) && !string.IsNullOrEmpty(u.Firstname) && !string.IsNullOrEmpty(u.Surename));

			UserHistoric = _seminarService.GetUserHistoric(userId);
			Certificates = _certificateService.GetCertificates(userId);

			return Page();
		}
	}
}
