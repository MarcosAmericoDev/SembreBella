using Microsoft.AspNetCore.Mvc.RazorPages;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;

namespace SempreBella.Pages.Administrador
{
    public class HistoricoPedidosModel : PageModel
    {
        private readonly IHistoricoPedidoService _historicoService;

        public HistoricoPedidosModel(IHistoricoPedidoService historicoService)
        {
            _historicoService = historicoService;
        }

        public List<PedidoHistoricoDTO> Pedidos { get; set; } = new();

        public async Task OnGet()
        {
            Pedidos = await _historicoService.ObterHistoricoAsync();
        }
    }
}
