using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;
using System.Security.Claims;

namespace SempreBella.Pages.Administrador
{
    public class LoginModel : PageModel
    {
        private IAuthService _authService;
        [BindProperty]
        public LoginInputModel Input { get; set; } = new LoginInputModel();

        public string? ErrorMessage { get; set; }

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult OnGet(string? returnURL = null)
        {
            if (User.Identity!.IsAuthenticated) 
            {
                return RedirectToPage("./Index");
            }

            ViewData["ReturnURL"] = returnURL;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnURL = null)
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Por favor, corrija os erros no formulário.";
                return Page();
            }

            ClaimsPrincipal? claimsPrincipal = await _authService.AuthenticateUserAsync(Input);

            if (claimsPrincipal == null)
            {
                ModelState.AddModelError(string.Empty, "Email ou senha inválidos");
                return Page();
            }

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties);

            if (Url.IsLocalUrl(returnURL))
            {
                return Redirect(returnURL);
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }
    }
}
