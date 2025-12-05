using SempreBella.Model;
using SempreBella.ViewModels;

namespace SempreBella.Services.Interfaces
{
    public interface IPedidoService
    {
        Task SalvarPedidoAsync(Pedido pedido);
    }
}
