using Microsoft.EntityFrameworkCore;
using SempreBella.Model;
using System.Data.Common;

namespace SempreBella.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Roupa> Roupas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
