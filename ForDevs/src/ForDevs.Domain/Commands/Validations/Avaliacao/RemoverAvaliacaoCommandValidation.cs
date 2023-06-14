using ForDevs.Domain.Commands.Avaliacao;

namespace ForDevs.Domain.Commands.Validations.Avaliacao
{
    public class RemoverAvaliacaoCommandValidation : AvaliacaoValidation<RemoverAvaliacaoCommand>
    {
        public RemoverAvaliacaoCommandValidation()
        {
            ValidarId();
        }
    }
}
