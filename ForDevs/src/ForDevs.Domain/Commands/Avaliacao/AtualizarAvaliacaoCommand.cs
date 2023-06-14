using ForDevs.Domain.Commands.Validations.Avaliacao;

namespace ForDevs.Domain.Commands.Avaliacao
{
    public class AtualizarAvaliacaoCommand : AvaliacaoCommand
    {
        public override bool EhValido()
        {
            ValidationResult = new AtualizarAvaliacaoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
