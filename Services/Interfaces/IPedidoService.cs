using SempreBella.Model;

namespace SempreBella.Services.Interfaces
{
    public interface IPedidoService
    {
        Task SalvarPedidoAsync(Pedido pedido);
    }
}
