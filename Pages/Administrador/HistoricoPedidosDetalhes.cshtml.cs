using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;

namespace SempreBella.Pages.Administrador
{
    public class HistoricoPedidosDetalhesModel : PageModel
    {
        private readonly IHistoricoPedidoService _historicoService;

        public HistoricoPedidosDetalhesModel(IHistoricoPedidoService historicoService)
        {
            _historicoService = historicoService;
        }

        public PedidoHistoricoDTO? Pedido { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Pedido = await _historicoService.ObterPorIdAsync(id);

            if (Pedido == null)
                return NotFound();

            return Page();
        }
    }
}
