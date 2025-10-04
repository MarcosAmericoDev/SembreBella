using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Services;

namespace SempreBella.Pages.Administrador
{
    public class DetailsModel : PageModel
    {
        private readonly IRoupaService _roupaService;

        public DetailsModel(IRoupaService roupaService)
        {
            _roupaService = roupaService;
        }

        public Roupa Roupa { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var roupa = await _roupaService.GetByIdAsync(id.Value);

            if (roupa is null) return NotFound();

            Roupa = roupa;
            return Page();
        }
    }
}
