using SempreBella.Model;
using SempreBella.Repositories.Interfaces;
using SempreBella.Services.Interfaces;

namespace SempreBella.Services.Implementations
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;   
        }

        public async Task SalvarPedidoAsync(Pedido pedido)
        {
            await _pedidoRepository.AddAsync(pedido);
            await _pedidoRepository.SaveChangesAsync();
        }
    }
}
