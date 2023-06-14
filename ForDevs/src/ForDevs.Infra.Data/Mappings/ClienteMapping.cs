
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ForDevs.Domain.Models;

namespace ForDevs.Infra.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Ignore(d => d.UltimaAvaliacao);

            builder.Property(c => c.NomeDoCliente)
              .IsRequired()
              .HasColumnType("varchar(100)");

            builder.Property(c => c.NomeContato)
              .IsRequired()
              .HasColumnType("varchar(100)");

            builder.Property(c => c.Cnpj)
              .HasColumnType("varchar(100)");
        }
    }
}
