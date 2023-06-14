using ForDevs.Domain.Core.Communication.Messages;
using ForDevs.Domain.Enums;

namespace ForDevs.Domain.Commands.Cliente
{
    public class ClienteCommand : Command
    {
        public Guid Id { get; protected set; }
        public string NomeDoCliente { get; protected set; }
        public string NomeContato { get; protected set; }
        public string Cnpj { get; protected set; }
        public Categoria Categoria { get; protected set; }
    }
}
