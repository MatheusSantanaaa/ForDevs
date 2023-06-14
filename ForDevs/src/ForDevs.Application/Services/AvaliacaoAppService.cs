using AutoMapper;
using FluentValidation.Results;
using ForDevs.Application.Dtos.Avaliacao;
using ForDevs.Application.Interfaces;
using ForDevs.Domain.Commands.Avaliacao;
using ForDevs.Domain.Commands.Cliente;
using ForDevs.Domain.Core.Communication.Mediator;
using ForDevs.Domain.Interfaces;

namespace ForDevs.Application.Services
{
    public class AvaliacaoAppService : IAvaliacaoAppService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public AvaliacaoAppService(IAvaliacaoRepository avaliacaoRepository,
                                   IMapper mapper,
                                   IMediatorHandler mediator)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _mapper = mapper;
            _mediator = mediator;
        }


        public async Task<ICollection<AvaliacaoDto>> ObterLista()
        {
            var avaliacoes = await _avaliacaoRepository.ObterPorLista();
            var avaliacoesDto = _mapper.Map<ICollection<AvaliacaoDto>>(avaliacoes);
            return avaliacoesDto;
        }

        public async Task<AvaliacaoDto> ObterPorId(Guid id)
        {
            return _mapper.Map<AvaliacaoDto>(await _avaliacaoRepository.ObterPorId(id));
        }

        public async Task<ValidationResult> Registrar(RegistrarAvaliacaoDto avaliacaoViewModel)
        {
            var command = _mapper.Map<RegistrarAvaliacaoCommand>(avaliacaoViewModel);
            return await _mediator.EnviarComando(command);
        }

        public async Task<ValidationResult> Atualizar(AtualizarAvaliacaoDto avaliacaoViewModel)
        {
            var command = _mapper.Map<AtualizarAvaliacaoCommand>(avaliacaoViewModel);
            return await _mediator.EnviarComando(command);
        }

        public async Task<ValidationResult> Remover(Guid id)
        {
            var command = new RemoverAvaliacaoCommand(id);
            return await _mediator.EnviarComando(command);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
