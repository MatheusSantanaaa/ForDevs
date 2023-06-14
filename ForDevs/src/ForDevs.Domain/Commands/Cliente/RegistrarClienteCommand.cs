using ForDevs.Domain.Commands.Validations.Cliente;

namespace ForDevs.Domain.Commands.Cliente
{
    public class RegistrarClienteCommand : ClienteCommand
    {
        public override bool EhValido()
        {
            ValidationResult = new RegistrarClienteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
