using Biblioteca.Context.Mappings;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

namespace Biblioteca.Context
{
    public class BibliotecaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Biblioteca ;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new GeneroMap());
            modelBuilder.ApplyConfiguration(new EditoraMap());
        }
        public DbSet<Biblioteca.Models.Livro> Livro { get; set; }
        public DbSet<Biblioteca.Models.Editora> Editora { get; set; }
        public DbSet<Biblioteca.Models.Genero> Genero { get; set; }
    }
}
