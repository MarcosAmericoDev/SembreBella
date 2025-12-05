using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;

namespace SempreBella.Pages.Administrador
{
    public class RelatoriosModel : PageModel
    {
        private readonly ITendenciasService _tendenciasService;

        public RelatoriosModel(ITendenciasService tendenciasService)
        {
            _tendenciasService = tendenciasService;
        }

        public List<TendenciaDTO> Tendencias { get; set; } = new();

        public async Task OnGet()
        {
            Tendencias = await _tendenciasService.GerarTendenciasAsync();
        }
    }
}
