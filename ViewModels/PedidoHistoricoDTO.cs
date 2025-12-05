using SempreBella.Model;

namespace SempreBella.ViewModels
{
    public class PedidoHistoricoDTO
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = "";
        public string Telefone { get; set; } = "";
        public DateTime DataPedido { get; set; }
        public decimal Total { get; set; }
        public List<ItemPedido> Itens { get; set; } = new();
    }
}
