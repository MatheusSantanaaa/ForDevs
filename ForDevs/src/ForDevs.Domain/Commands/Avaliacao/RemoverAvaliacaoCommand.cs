using ForDevs.Domain.Commands.Validations.Avaliacao;

namespace ForDevs.Domain.Commands.Avaliacao
{
    public class RemoverAvaliacaoCommand : AvaliacaoCommand
    {
        public RemoverAvaliacaoCommand(Guid id)
        {
            Id = id;
        }
        public override bool EhValido()
        {
            ValidationResult = new RemoverAvaliacaoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

}
