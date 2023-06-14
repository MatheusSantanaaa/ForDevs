using ForDevs.Domain.Commands.Validations.Avaliacao;

namespace ForDevs.Domain.Commands.Avaliacao
{
    public class RegistrarAvaliacaoCommand : AvaliacaoCommand
    {
        public override bool EhValido()
        {
            ValidationResult = new RegistrarAvaliacaoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

}
