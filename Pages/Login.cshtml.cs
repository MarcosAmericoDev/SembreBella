using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SempreBella.Pages
{
    /// <summary>
    /// Code-behind para a p�gina de Login. Processa credenciais e realiza a autentica��o via cookies.
    /// </summary>
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; } = new LoginInputModel();

       [TempData]
        public string? Message { get; set; }

        [TempData]
        public string? SuccessMessage { get; set; }


        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SuccessMessage))
            {
                Message = SuccessMessage;
            }
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("/Index");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            ClaimsPrincipal? principal = await _authService.AuthenticateUserAsync(Input);

            if (principal == null)
            {
                ModelState.AddModelError(string.Empty, "Credenciais inv�lidas. Verifique o email e a senha.");
                Message = "Falha no login. Credenciais incorretas.";
                return Page();
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false
                });

            return LocalRedirect(returnUrl);
        }
    }
}