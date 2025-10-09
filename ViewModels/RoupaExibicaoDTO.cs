using SempreBella.Model.Enums;

namespace SempreBella.ViewModels
{
    public class RoupaExibicaoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public CategoriaRoupa Categoria { get; set; }
        public string PrecoOriginalFormatado { get; set; } = string.Empty;
        public int? Desconto { get; set; }
        public string PrecoFinalFormatado { get; set; } = string.Empty;
        public bool TemDesconto => Desconto.HasValue && Desconto.Value > 0;

        public int Estoque { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
        public bool EstaAtiva { get; set; }
    }
}
