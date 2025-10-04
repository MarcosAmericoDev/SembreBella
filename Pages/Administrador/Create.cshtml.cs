using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Model.Enums;
using SempreBella.Services;

namespace SempreBella.Pages.Administrador
{
    public class CreateModel : PageModel
    {
        private readonly IRoupaService _roupaService;

        public CreateModel(IRoupaService roupaService)
        {
            _roupaService = roupaService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Roupa Roupa { get; set; } = new Roupa
        {
            Categoria = CategoriaRoupa.NaoSelecionado
        };

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _roupaService.CreateAsync(Roupa);

            return RedirectToPage("./Index");
        }
    }
}
