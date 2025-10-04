using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;

namespace SempreBella.Pages.Administrador
{
    public class IndexModel : PageModel
    {
        private readonly SempreBella.Data.ApplicationDbContext _context;

        public IndexModel(SempreBella.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Roupa> Roupa { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Roupa = await _context.Roupas.ToListAsync();
        }
    }
}
