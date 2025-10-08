using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SempreBella.Model;
using SempreBella.Services.Interfaces;
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

        public IList<Roupa> Roupas { get; set; } = new List<Roupa>();

        public async Task OnGet()
        {
            var todasAsRoupas = await _roupaService.GetAllAsync();

            Roupas = todasAsRoupas.Where(r => r.EstaAtiva == true).ToList();
        }
    }
}
