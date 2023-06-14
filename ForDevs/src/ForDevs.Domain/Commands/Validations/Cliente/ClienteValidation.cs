using FluentValidation;
using ForDevs.Domain.Commands.Cliente;
using ForDevs.Domain.Core.Utils;

namespace ForDevs.Domain.Commands.Validations.Cliente
{
    public class ClienteValidation<T> : AbstractValidator<T> where T : ClienteCommand
    {
        protected void ValidarId()
        {
            RuleFor(c => c.Id)
             .NotEqual(Guid.Empty)
             .WithMessage("O Id é obrigatório.");
        }

        protected void ValidarNomeContato()
        {
            RuleFor(c => c.NomeContato)
                .NotEmpty()
                .WithMessage("O nome do contato é obrigatório.");
        }

        protected void ValidarNomeCliente()
        {
            RuleFor(c => c.NomeDoCliente)
                .NotEmpty()
                .WithMessage("O nome do cliente é obrigatório.");
        }

        protected void ValidarCnpj()
        {

            RuleFor(c => c.Cnpj)
               .Must(HasValidarCpf)
               .When(c => !string.IsNullOrEmpty(c.Cnpj))
               .WithMessage("O CNPJ informado não é valido.");
        }

        protected static bool HasValidarCpf(string cpf)
        {
            return CnpjValidacao.EhCnpj(cpf);
        }
    }
}
