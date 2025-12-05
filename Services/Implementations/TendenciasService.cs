using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Services.Interfaces;
using SempreBella.ViewModels;

namespace SempreBella.Services.Implementations
{
    public class TendenciasService : ITendenciasService
    {
        private readonly ApplicationDbContext _context;
        public TendenciasService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TendenciaDTO>> GerarTendenciasAsync()
        {
            DateTime hoje = DateTime.Now;
            DateTime periodoAtual = hoje.AddDays(-30);
            DateTime periodoAnterior = hoje.AddDays(-60);

            var itens = await _context.ItensPedido
                .Include(i => i.Pedido)
                .ToListAsync();

            // vendas no mês atual por categoria
            var atual = itens
                .Where(i => i.Pedido.DataPedido >= periodoAtual)
                .GroupBy(i => i.NomeProduto)
                .Select(g => new { Produto = g.Key, Total = g.Sum(i => i.Quantidade) })
                .ToList();

            // vendas do mês anterior por categoria
            var anterior = itens
                .Where(i => i.Pedido.DataPedido >= periodoAnterior && i.Pedido.DataPedido < periodoAtual)
                .GroupBy(i => i.NomeProduto)
                .Select(g => new { Produto = g.Key, Total = g.Sum(i => i.Quantidade) })
                .ToList();

            var tendencias = new List<TendenciaDTO>();

            foreach (var a in atual)
            {
                var prev = anterior.FirstOrDefault(p => p.Produto == a.Produto)?.Total ?? 0;

                if (prev == 0) continue;

                double variacao = ((double)a.Total - prev) / prev * 100;

                if (Math.Abs(variacao) >= 5) // só adiciona se relevante
                {
                    tendencias.Add(new TendenciaDTO
                    {
                        Titulo = a.Produto,
                        VariacaoPercentual = Math.Round(variacao, 1)
                    });
                }
            }

            return tendencias.OrderByDescending(t => Math.Abs(t.VariacaoPercentual)).ToList();
        }
    }
}
