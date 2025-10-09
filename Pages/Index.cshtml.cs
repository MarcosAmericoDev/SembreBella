using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language;
using SempreBella.Model;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;
using System.Threading.Tasks;

namespace SempreBella.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRoupaService _roupaService;

        public IndexModel(IRoupaService roupaService)
        {
            _roupaService = roupaService;
        }

        public IList<RoupaExibicaoDTO> Roupas { get; set; } = new List<RoupaExibicaoDTO>();

        public async Task OnGet()
        {
            Roupas = await _roupaService.GetAllAtivasAsync();
        }
    }
}
