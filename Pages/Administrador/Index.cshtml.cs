using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Services.Interfaces;

namespace SempreBella.Pages.Administrador
{
    [Authorize(Roles = "Administrador")]
    public class IndexModel : PageModel
    {
        private readonly IRoupaService _roupaService;

        public IndexModel(IRoupaService roupaService)
        {
            _roupaService = roupaService;
        }

        public IList<Roupa> Roupa { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Roupa = await _roupaService.GetAllAsync();
        }
    }
}
