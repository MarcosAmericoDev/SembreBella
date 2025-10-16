using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SempreBella.Model;
using SempreBella.Services.Interfaces;

namespace SempreBella.Pages.Api
{
    [IgnoreAntiforgeryToken]
    public class SalvarPedidoModel : PageModel
    {
        private readonly IPedidoService _pedidoService;

        public SalvarPedidoModel(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task<IActionResult> OnPostAsync([FromBody] Pedido pedido)
        {
            Console.WriteLine($"Recebido pedido de {pedido.NomeCliente} com {pedido.Itens.Count} itens e total R${pedido.ValorTotal}");
            if (pedido == null || !pedido.Itens.Any())
                return BadRequest("Pedido inválido.");

            await _pedidoService.SalvarPedidoAsync(pedido);
            return new JsonResult(new { sucesso = true });
        }
    }
}
