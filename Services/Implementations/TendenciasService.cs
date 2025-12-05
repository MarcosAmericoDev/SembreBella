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

            var roupasAtivas = await _context.Roupas
                .Where(r => r.EstaAtiva == true)
                .ToDictionaryAsync(r => r.Id, r => r);

            var itens = await _context.ItensPedido
                .Include(i => i.Pedido)
                .ToListAsync();

            itens = itens.Where(i => roupasAtivas.ContainsKey(i.RoupaId)).ToList();

            var atual = itens
                .Where(i => i.Pedido.DataPedido >= periodoAtual)
                .GroupBy(i => i.NomeProduto)
                .Select(g => new { Nome = g.Key, Total = g.Sum(x => x.Quantidade) })
                .ToList();


            // Mês anterior
            var anterior = itens
                .Where(i => i.Pedido.DataPedido >= periodoAnterior && i.Pedido.DataPedido < periodoAtual)
                .GroupBy(i => i.NomeProduto)
                .Select(g => new { Nome = g.Key, Total = g.Sum(x => x.Quantidade) })
                .ToList();

            List<TendenciaDTO> tendencias = new();

            foreach (var a in atual)
            {
                var prev = anterior.FirstOrDefault(p => p.Nome == a.Nome)?.Total ?? 0;

                if (prev == 0) continue;

                double variacao = ((double)a.Total - prev) / prev * 100;

                if (Math.Abs(variacao) >= 5)
                {
                    tendencias.Add(new TendenciaDTO
                    {
                        Titulo = a.Nome,
                        VariacaoPercentual = Math.Round(variacao, 1)
                    });
                }
            }

            return tendencias.OrderByDescending(t => Math.Abs(t.VariacaoPercentual)).ToList();
        }
    }
}
