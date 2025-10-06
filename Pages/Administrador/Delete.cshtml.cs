using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Services.Interfaces;

namespace SempreBella.Pages.Administrador
{
    public class DeleteModel : PageModel
    {
        private readonly IRoupaService _roupaService;

        public DeleteModel(IRoupaService roupaService)
        {
            _roupaService = roupaService;
        }

        [BindProperty]
        public Roupa Roupa { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();


            var roupa = await _roupaService.GetByIdAsync(id.Value);

            if (roupa is null) return NotFound();

            Roupa = roupa;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            await _roupaService.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
