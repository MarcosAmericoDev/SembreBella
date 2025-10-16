using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace SempreBella.Model
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeCliente { get; set; } = string.Empty;
        [Required]
        public string Telefone { get; set; } = string.Empty;
        [Required]
        public string Estado { get; set; } = string.Empty;
        [Required]
        public string Cidade { get; set; } = string.Empty;
        [Required]
        public string Rua { get; set; } = string.Empty;
        [Required]
        public string Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; }

        [Required]
        public decimal ValorTotal { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;

        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }

    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoupaId { get; set; }
        [Required]
        public string NomeProduto { get; set; } = string.Empty;
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal PrecoUnitario { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = default!;
    }
}
