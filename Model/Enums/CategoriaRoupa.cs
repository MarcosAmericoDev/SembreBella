using System.ComponentModel.DataAnnotations;

namespace SempreBella.Model.Enums
{
    public enum CategoriaRoupa
    {
        [Display(Name = "-- Escolha uma Categoria --")]
        NaoSelecionado = 0,
        Alcinha = 1,
        Camisa = 2, 
        Bufante = 3, 
        Lisa = 4,
        Cropped = 5, 
        Babado = 6
    }
}
