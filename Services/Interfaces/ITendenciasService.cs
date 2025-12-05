using SempreBella.ViewModels;

namespace SempreBella.Services.Interfaces
{
    public interface ITendenciasService
    {
        Task<List<TendenciaDTO>> GerarTendenciasAsync();
    }
}
