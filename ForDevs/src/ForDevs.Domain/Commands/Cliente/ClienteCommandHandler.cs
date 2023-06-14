using FluentValidation.Results;
using ForDevs.Domain.Core.Communication.Messages;
using ForDevs.Domain.Interfaces;
using MediatR;

namespace ForDevs.Domain.Commands.Cliente
{
    public class ClienteCommandHandler : CommandHandler,
      IRequestHandler<RegistrarClienteCommand, ValidationResult>,
      IRequestHandler<AtualizarClienteCommand, ValidationResult>,
      IRequestHandler<RemoverClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository, IAvaliacaoRepository avaliacaoRepository)
        {
            _clienteRepository = clienteRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            await ValidarCnpj(message.Cnpj);

            if(ValidationResult.Errors.Count() > 0)
            {
                return ValidationResult;
            }

            var novoCliente = new Models.Cliente(message.NomeDoCliente, message.NomeContato, message.Cnpj);

            _clienteRepository.Adicionar(novoCliente);

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = await _clienteRepository.ObterPorId(message.Id);

            if(cliente is null)
            {
                base.AdicionarErro("Cliente inválido ou inexistente.");
                return ValidationResult;
            }

            cliente.Atualizar(message.NomeDoCliente, message.NomeContato, message.Cnpj);

            _clienteRepository.Atualizar(cliente);

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoverClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = await _clienteRepository.ObterPorId(message.Id);

            if (cliente is null)
            {
                AdicionarErro("Cliente inválido ou inexistente.");
                return ValidationResult;
            }

            _avaliacaoRepository.Remover(cliente.AvaliacaoClientes);
            _clienteRepository.Remover(cliente);

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        private async Task<ValidationResult> ValidarCnpj(string? cnpj)
        {
            if (!string.IsNullOrEmpty(cnpj))
            {
                var cliente = await _clienteRepository.ObterPorCnpj(cnpj);

                if (cliente != null)
                {
                    AdicionarErro("Um cliente já foi cadastrado com esse mesmo CNPJ.");
                }
            }

            return ValidationResult;
        }
    }

}
