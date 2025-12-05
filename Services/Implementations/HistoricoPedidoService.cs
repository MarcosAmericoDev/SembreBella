using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;

namespace SempreBella.Services.Implementations
{
    public class HistoricoPedidoService : IHistoricoPedidoService
    {
        private readonly ApplicationDbContext _context;

        public HistoricoPedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PedidoHistoricoDTO>> ObterHistoricoAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .OrderByDescending(p => p.DataPedido)
                .Select(p => new PedidoHistoricoDTO
                {
                    Id = p.Id,
                    NomeCliente = p.NomeCliente,
                    Telefone = p.Telefone,
                    DataPedido = p.DataPedido,
                    Total = p.Itens.Sum(i => i.PrecoUnitario * i.Quantidade),
                    Itens = p.Itens.ToList()
                })
                .ToListAsync();
        }

        public async Task<PedidoHistoricoDTO?> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.Id == id)
                .Select(p => new PedidoHistoricoDTO
                {
                    Id = p.Id,
                    NomeCliente = p.NomeCliente,
                    Telefone = p.Telefone,
                    DataPedido = p.DataPedido,
                    Total = p.Itens.Sum(i => i.PrecoUnitario * i.Quantidade),
                    Itens = p.Itens.ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
