using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;
using SempreBella.Constants;

namespace SempreBella.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _authService;

        [TempData]
        public string? Message { get; set; }

        public RegisterModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; } = new RegisterInputModel();

        public void OnGet()
        {
            Input.Papel = RoleConstants.Cliente;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "Erro no formul�rio de registro. Verifique os campos.";
                return Page();
            }

            var newUser = await _authService.RegisterUserAsync(Input);

            if (newUser == null)
            {
                ModelState.AddModelError(nameof(Input.Email), "Este email j� est� registrado.");
                Message = "O registro falhou. Email j� em uso.";
                return Page();
            }

            Message = "Registro efetuado com sucesso! Voc� pode fazer login agora.";
            return RedirectToPage("/Login");
        }
    }
}
