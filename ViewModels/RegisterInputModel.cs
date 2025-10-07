using System.ComponentModel.DataAnnotations;
using SempreBella.Constants; // Importando a classe constante
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace SempreBella.ViewModels
{
    /// <summary>
    /// ViewModel utilizada para a página de registro de novos usuários.
    /// </summary>
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres de comprimento.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [Compare("Senha", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O papel é obrigatório.")]
        [Display(Name = "Tipo de Usuário")]
        public string Papel { get; set; } = RoleConstants.Cliente;

        public static List<SelectListItem> PapelOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = RoleConstants.Cliente, Text = "Cliente" },
            new SelectListItem { Value = RoleConstants.Administrador, Text = "Administrador" }
        };
    }
}
