// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using ChristianBookClub.Data;
using ChristianBookClub.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Número de Celular")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Nome")]
            public string Firstname { get; set; } = string.Empty;

            [Display(Name = "Sobrenome")]
            public string Surename { get; set; } = string.Empty;

            [Display(Name = "Telegram Id")]
            public string TelegramId { get; set; } = string.Empty;

        }

        private async Task LoadAsync(User user)
        {
            user ??= await _userManager.GetUserAsync(User);
            Input = new InputModel
            {
                Firstname = user.Firstname,
                Surename = user.Surename,
                TelegramId = user.TelegramId,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar dados do usuário com o Id: '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar dados do usuário com o Id: '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.PhoneNumber != user.PhoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Erro inesperado ao configurar número telefônico.";
                    return RedirectToPage();
                }
            }

            if (Input.Firstname != user.Firstname)
            {
                user.Firstname = Input.Firstname;
                var result = _context.SaveChanges();
                if (result == 0)
                {
                    StatusMessage = "Erro inesperado ao configurar primeiro nome.";
                    return RedirectToPage();
                }
            }

            if (Input.Surename != user.Surename)
            {
                user.Surename = Input.Surename;
                var result = _context.SaveChanges();
                if (result == 0)
                {
                    StatusMessage = "Erro inesperado ao configurar o Sobrenome.";
                    return RedirectToPage();
                }
            }

            if (Input.TelegramId != user.TelegramId)
            {
                user.TelegramId = Input.TelegramId;
                var result = _context.SaveChanges();
                if (result == 0)
                {
                    StatusMessage = "Erro inesperado ao configurar o Telegram Id.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Seu perfil foi atualizado.";
            return RedirectToPage();
        }
    }
}
