using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Context.Mappings
{
    public class EditoraMap : IEntityTypeConfiguration<Editora>
    {
        public void Configure(EntityTypeBuilder<Editora> builder)
        {
            builder.ToTable(nameof(Editora));
            builder.HasKey(x => x.Id);
        }
    }
}
