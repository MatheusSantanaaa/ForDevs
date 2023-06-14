using ForDevs.Domain.Commands.Avaliacao;

namespace ForDevs.Domain.Commands.Validations.Avaliacao
{
    public class RegistrarAvaliacaoCommandValidation : AvaliacaoValidation<RegistrarAvaliacaoCommand>
    {
        public RegistrarAvaliacaoCommandValidation()
        {
            ValidarAnoReferencia();
            ValidarClientes();
            ValidarNota();
            ValidarMotivoDaNota();
        }
    }
}
