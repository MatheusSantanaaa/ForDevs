using ForDevs.Domain.Commands.Cliente;

namespace ForDevs.Domain.Commands.Validations.Cliente
{
    public class RegistrarClienteCommandValidation : ClienteValidation<RegistrarClienteCommand>
    {
        public RegistrarClienteCommandValidation()
        {
            ValidarNomeCliente();
            ValidarNomeContato();
            ValidarCnpj();
        }
    }
}
