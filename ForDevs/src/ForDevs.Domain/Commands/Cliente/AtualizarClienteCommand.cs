using ForDevs.Domain.Commands.Validations.Cliente;

namespace ForDevs.Domain.Commands.Cliente
{
    public class AtualizarClienteCommand : ClienteCommand
    {
        public override bool EhValido()
        {
            ValidationResult = new AtualizarClienteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

}
