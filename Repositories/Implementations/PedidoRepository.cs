using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Repositories.Interfaces;

namespace SempreBella.Repositories.Implementations
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
