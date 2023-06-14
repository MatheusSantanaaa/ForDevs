
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ForDevs.Domain.Models;

namespace ForDevs.Infra.Data.Mappings
{
    public class AvaliacaoClienteMapping : IEntityTypeConfiguration<AvaliacaoCliente>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoCliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.MotivoNota)
              .IsRequired()
              .HasColumnType("varchar(100)");

            builder.Property(c => c.Nota)
             .IsRequired()
             .HasColumnType("decimal(3,1)");
        }
    }
}
