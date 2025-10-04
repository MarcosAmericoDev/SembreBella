using SempreBella.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace SempreBella.Model
{
    public class Roupa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public CategoriaRoupa Categoria { get; set; }
        [Required]
        public double Preco { get; set; }
        [Required]
        public int Estoque { get; set; }
        [Required]
        public string ImagemUrl { get; set; }
        [Required]
        public bool EstaAtiva { get; set; }
        public int? Desconto { get; set; }
    }
}
