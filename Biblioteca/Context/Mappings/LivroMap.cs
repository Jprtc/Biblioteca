using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Context.Mappings
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable(nameof(Livro));
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Editora).WithMany().HasForeignKey(x => x.EditoraId);
            builder.HasOne(x => x.Genero).WithMany().HasForeignKey(x => x.GeneroId);
        }
    }
}
