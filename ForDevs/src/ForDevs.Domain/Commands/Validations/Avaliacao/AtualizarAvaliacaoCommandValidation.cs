using ForDevs.Domain.Commands.Avaliacao;

namespace ForDevs.Domain.Commands.Validations.Avaliacao
{
    public class AtualizarAvaliacaoCommandValidation : AvaliacaoValidation<AtualizarAvaliacaoCommand>
    {
        public AtualizarAvaliacaoCommandValidation()
        {
            ValidarId();
            ValidarAnoReferencia();
            ValidarClientes();
            ValidarNota();
            ValidarMotivoDaNota();
        }
    }
}
