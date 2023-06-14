using ForDevs.Domain.Commands.Cliente;

namespace ForDevs.Domain.Commands.Validations.Cliente
{
    public class AtualizarClienteCommandValidation : ClienteValidation<AtualizarClienteCommand>
    {
        public AtualizarClienteCommandValidation()
        {
            ValidarId();
            ValidarCnpj();
        }
    }
}
