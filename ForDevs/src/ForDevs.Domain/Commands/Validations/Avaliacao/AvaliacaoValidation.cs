using FluentValidation;
using ForDevs.Domain.Commands.Avaliacao;
using ForDevs.Domain.Commands.Cliente;
using ForDevs.Domain.Commands.Validations.Cliente;

namespace ForDevs.Domain.Commands.Validations.Avaliacao
{
    public class AvaliacaoValidation<T> : AbstractValidator<T> where T : AvaliacaoCommand
    {
        protected void ValidarId()
        {
            RuleFor(c => c.Id)
             .NotEqual(Guid.Empty)
             .WithMessage("O Id é obrigatório.");
        }

        protected void ValidarAnoReferencia()
        {
            RuleFor(c => c.DataDeReferencia)
             .NotEmpty()
             .WithMessage("A data de referência é obrigatório.");
        }

        protected void ValidarClientes()
        {
            RuleFor(c => c.AvaliacaoClientes)
             .Must(c => c?.Count > 0)
             .WithMessage("A avaliação deve conter ao menos um cliente.");
        }

        protected void ValidarNota()
        {
            RuleForEach(a => a.AvaliacaoClientes)
            .ChildRules(avaliacaoCliente =>
            {
                avaliacaoCliente.RuleFor(ac => ac.Nota)
                    .InclusiveBetween(0, 10)
                    .WithMessage("A nota deve estar entre 0 e 10.");

                avaliacaoCliente.RuleFor(ac => ac.MotivoNota)
                    .NotEmpty()
                    .WithMessage("O motivo da nota deve ser informado.");
            });
        }

        protected void ValidarMotivoDaNota()
        {
            RuleForEach(a => a.AvaliacaoClientes)
            .ChildRules(avaliacaoCliente =>
            {
                avaliacaoCliente.RuleFor(ac => ac.MotivoNota)
                    .NotEmpty()
                    .WithMessage("O motivo da nota deve ser informado.");
            });
        }
    }
}
