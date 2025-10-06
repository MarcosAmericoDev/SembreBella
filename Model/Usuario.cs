using System.ComponentModel.DataAnnotations;

namespace SempreBella.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        [Required]
        public string Papel { get; set; } = "Cliente";
    }
}
