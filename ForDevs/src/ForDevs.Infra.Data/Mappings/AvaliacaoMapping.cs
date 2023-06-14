
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ForDevs.Domain.Models;

namespace ForDevs.Infra.Data.Mappings
{
    public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Ignore(d => d.ResultadoGeral);
            builder.Ignore(d => d.QuantidadeDePromotores);
            builder.Ignore(d => d.QuantidadeDeNeutros);
            builder.Ignore(d => d.QuantidadeDeDetratores);
            builder.Ignore(d => d.TotalDeParticipantes);
        }
    }
}
