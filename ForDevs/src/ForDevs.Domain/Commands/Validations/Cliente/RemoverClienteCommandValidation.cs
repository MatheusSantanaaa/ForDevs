using ForDevs.Domain.Commands.Cliente;

namespace ForDevs.Domain.Commands.Validations.Cliente
{
    public class RemoverClienteCommandValidation : ClienteValidation<RemoverClienteCommand>
    {
        public RemoverClienteCommandValidation()
        {
            ValidarId();
        }
    }
}
