using ForDevs.Domain.Core.Communication.Messages;
using ForDevs.Domain.Models;

namespace ForDevs.Domain.Commands.Avaliacao
{
    public class AvaliacaoCommand : Command
    {
        public Guid Id { get; protected set; }
        public DateTime DataDeReferencia { get; protected set; }
        public ICollection<AvaliacaoCliente> AvaliacaoClientes { get; protected set; }
    }
}
