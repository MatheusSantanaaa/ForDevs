using ForDevs.Domain.Commands.Validations.Cliente;

namespace ForDevs.Domain.Commands.Cliente
{
    public class RemoverClienteCommand : ClienteCommand
    {
        public RemoverClienteCommand(Guid id)
        {
            Id = id;
        }
        public override bool EhValido()
        {
            ValidationResult = new RemoverClienteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
