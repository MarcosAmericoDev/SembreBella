using Microsoft.EntityFrameworkCore;
using SempreBella.Model;
using SempreBella.Services.Interfaces;
using SempreBella.Repositories.Interfaces;

namespace SempreBella.Services.Implementations
{
    public class RoupaService : IRoupaService
    {
        private readonly IRoupaRepository _roupaRepository = default!;

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

        public async Task<Roupa?> GetByIdAsync(int id)
        {
            var roupasEncontrada = await _roupaRepository.FindAsync(x => x.Id == id);
            return roupasEncontrada.FirstOrDefault();
        }

        public async Task UpdateAsync(Roupa roupa)
        {
            await _roupaRepository.UpdateAsync(roupa);
        }

        public async Task<List<Roupa>> GetAllAtivasAsync()
        {
            return await _roupaRepository.FindAsync(x => x.EstaAtiva == true);
        }
    }
}
