using SempreBella.ViewModels;

namespace SempreBella.Services.Interfaces
{
    public interface IHistoricoPedidoService
    {
        Task<List<PedidoHistoricoDTO>> ObterHistoricoAsync();
        Task<PedidoHistoricoDTO?> ObterPorIdAsync(int id);
    }
}
