using FluentValidation.Results;
using ForDevs.Domain.Core.Communication.Messages;
using ForDevs.Domain.Interfaces;
using ForDevs.Domain.Models;
using MediatR;

namespace ForDevs.Domain.Commands.Avaliacao
{
    public class AvaliacaoCommandHandler : CommandHandler,
     IRequestHandler<RegistrarAvaliacaoCommand, ValidationResult>,
     IRequestHandler<AtualizarAvaliacaoCommand, ValidationResult>,
     IRequestHandler<RemoverAvaliacaoCommand, ValidationResult>
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IClienteRepository _clienteRepository;

        public AvaliacaoCommandHandler(IAvaliacaoRepository avaliacaoRepository, 
                                       IClienteRepository clienteRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarAvaliacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var ultimaAvaliacao = await _avaliacaoRepository.ObterUltimaAvaliacao();
           
            if (ultimaAvaliacao != null)
            {
                DateTime mesAtual = DateTime.Now;
                if (ultimaAvaliacao.DataDeCriacao.Month == mesAtual.Month && ultimaAvaliacao.DataDeCriacao.Year == mesAtual.Year)
                {
                    AdicionarErro("Só é possivel fazer uma Avaliação por mês.");
                    return ValidationResult;
                }
            }


            var avaliacao = new Models.Avaliacao(message.DataDeReferencia);

            var avalicoesClientes = CriarAvaliacoesClientes(message, avaliacao.Id);
            avaliacao.AdicionarAvaliacaoClientes(avalicoesClientes);

            var clientes = await _clienteRepository.ObterClientesParaAtualizarCategoria(avaliacao.AvaliacaoClientes.Select(x => x.ClienteId).ToList());

            AtualizarCategoriasDoCliente(clientes, avalicoesClientes);

            _avaliacaoRepository.Adicionar(avaliacao);

            return await PersistirDados(_avaliacaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarAvaliacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var avaliacao = await _avaliacaoRepository.ObterPorId(message.Id);

            if(avaliacao is null)
            {
                AdicionarErro("Avaliação inválida ou inexistente");
                return ValidationResult;
            }

            avaliacao.Atualizar(message.DataDeReferencia);

            var avalicoesClientes = CriarAvaliacoesClientes(message, avaliacao.Id);

            _avaliacaoRepository.RemoverAvalicoesClientes(avaliacao.AvaliacaoClientes);
            _avaliacaoRepository.AdicionarAvalicoesClientes(avalicoesClientes);

            var clientesParaAtualizar = await _clienteRepository.ObterClientesParaAtualizarCategoria(avalicoesClientes.Select(x => x.ClienteId).ToList());

            AtualizarCategoriasDoCliente(clientesParaAtualizar, avalicoesClientes);

            return await PersistirDados(_avaliacaoRepository.UnitOfWork);
        }


        public async Task<ValidationResult> Handle(RemoverAvaliacaoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var avaliacao = await _avaliacaoRepository.ObterPorId(message.Id);

            if (avaliacao is null)
            {
                AdicionarErro("Avaliação inválida ou inexistente");
                return ValidationResult;
            }

            _avaliacaoRepository.RemoverAvalicoesClientes(avaliacao.AvaliacaoClientes);
            _avaliacaoRepository.Remover(avaliacao);

            return await PersistirDados(_avaliacaoRepository.UnitOfWork);
        }


        #region Metodos e Funcoes

        private void AtualizarCategoriasDoCliente(ICollection<Models.Cliente> clientes, List<AvaliacaoCliente> avaliacaoClientes)
        {
            foreach (var cliente in clientes)
            {
                var nota = (decimal)avaliacaoClientes.Where(x => x.ClienteId.Equals(cliente.Id)).Select(x => x.Nota).First();
                var categoria = cliente.ObterCategoriaPorNota(nota);

                cliente.AtualizarCategoria(categoria);
                _clienteRepository.Atualizar(cliente);
            }
        }

        private static List<AvaliacaoCliente> CriarAvaliacoesClientes(AvaliacaoCommand command, Guid avaliacaoId)
        {
            List<AvaliacaoCliente> avaliacoesClientes = new();

            if(command.AvaliacaoClientes.Any())
            {
                foreach(var avaliacaoCliente in command.AvaliacaoClientes)
                {
                    avaliacoesClientes.Add(
                        AvaliacaoCliente.Factory.CriarAvaliacaoCliente(avaliacaoId,
                                                                       avaliacaoCliente.ClienteId,
                                                                       avaliacaoCliente.Nota,
                                                                       avaliacaoCliente.MotivoNota));
                }
            }

            return avaliacoesClientes;
        }

        #endregion
    }

}
