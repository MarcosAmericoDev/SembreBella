namespace SempreBella.ViewModels
{
    public class TendenciaDTO
    {
        public string Titulo { get; set; } = "";
        public double VariacaoPercentual { get; set; }
        public bool EPositiva => VariacaoPercentual >= 0;
    }
}
