using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Services;

namespace SempreBella.Pages.Administrador
{
    public class EditModel : PageModel
    {
        private readonly IRoupaService _roupaService;

        public EditModel(IRoupaService roupaService)
        {
            _roupaService = roupaService;
        }

        [BindProperty]
        public Roupa Roupa { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roupa = await _roupaService.GetByIdAsync(id.Value);
            if (roupa == null)
            {
                return NotFound();
            }
            Roupa = roupa;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _roupaService.UpdateAsync(Roupa);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _roupaService.ExistsAsync(Roupa.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
