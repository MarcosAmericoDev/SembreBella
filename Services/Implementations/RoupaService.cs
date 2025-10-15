using Microsoft.EntityFrameworkCore;
using SempreBella.Model;
using SempreBella.Services.Interfaces;
using SempreBella.Repositories.Interfaces;
using System.Globalization;
using SempreBella.ViewModels;

namespace SempreBella.Services.Implementations
{
    public class RoupaService : IRoupaService
    {
        private readonly IRoupaRepository _roupaRepository = default!;
        private readonly CultureInfo _culture = new CultureInfo("pt-BR");

        public RoupaService(IRoupaRepository roupaRepository)
        {
            _roupaRepository = roupaRepository;
        }

        public async Task CreateAsync(Roupa roupa)
        {
            await _roupaRepository.AddAsync(roupa);
            await _roupaRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var roupa = await GetByIdAsync(id);

            if(roupa != null)
            {
                await _roupaRepository.Remove(roupa);
                await _roupaRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var count = await _roupaRepository.FindAsync(x => x.Id == id);
            return count.Any();
        }

        public Task<List<Roupa>> GetAllAsync()
        {
            return _roupaRepository.GetAllAsync();
        }

        private RoupaExibicaoDTO MapToDTO(Roupa roupa)
        {
            string precoOriginalFormatado = roupa.Preco.ToString("C2", _culture);
            double precoFinal = roupa.Preco;

            if (roupa.Desconto.HasValue && roupa.Desconto.Value > 0)
            {
                precoFinal = roupa.Preco * (1 - (roupa.Desconto.Value / 100.0));
            }
            string precoFinalFormatado = precoFinal.ToString("C2", _culture);

            return new RoupaExibicaoDTO
            {
                Id = roupa.Id,
                Nome = roupa.Nome,
                Categoria = roupa.Categoria,
                Estoque = roupa.Estoque,
                ImagemUrl = roupa.ImagemUrl,
                EstaAtiva = roupa.EstaAtiva,
                PrecoNumerico = roupa.Desconto.HasValue
                    ? (decimal)roupa.Preco * (1 - (roupa.Desconto.Value / 100m))
                    : (decimal)roupa.Preco,
                PrecoOriginalFormatado = precoOriginalFormatado,
                Desconto = roupa.Desconto,
                PrecoFinalFormatado = precoFinalFormatado
            };
        }

        public async Task<List<RoupaExibicaoDTO>> GetAllAtivasAsync()
        {
            var roupas = await _roupaRepository.FindAsync(x => x.EstaAtiva == true);

            return roupas.Select(MapToDTO).ToList();
        }

        public async Task<Roupa?> GetByIdAsync(int id)
        {
            var roupasEncontrada = await _roupaRepository.FindAsync(x => x.Id == id);
            return roupasEncontrada.FirstOrDefault();
        }

        public async Task UpdateAsync(Roupa roupa)
        {
            await _roupaRepository.UpdateAsync(roupa);
        }


    }
}
