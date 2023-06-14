using AutoMapper;
using FluentValidation.Results;
using ForDevs.Application.Dtos.Cliente;
using ForDevs.Application.Interfaces;
using ForDevs.Domain.Commands.Cliente;
using ForDevs.Domain.Core.Communication.Mediator;
using ForDevs.Domain.Interfaces;

namespace ForDevs.Application.Services
{
    public class CLienteAppService : IClienteAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IClienteRepository _clienteRepository;

        public CLienteAppService(IMapper mapper, 
                                 IMediatorHandler mediator, 
                                 IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Atualizar(AtualizarClienteDto clienteViewModel)
        {
            var command = _mapper.Map<AtualizarClienteCommand>(clienteViewModel);
            return await _mediator.EnviarComando(command);
        }


        public async Task<ICollection<ClienteDto>> ObterLista()
        {
            return _mapper.Map<ICollection<ClienteDto>>(await _clienteRepository.ObterLista());
        }

        public async Task<ClienteDto> ObterPorId(Guid id)
        {
            return _mapper.Map<ClienteDto>(await _clienteRepository.ObterPorId(id));
        }
        public async Task<ICollection<ClienteDto>> ObterPorNome(string nome)
        {
            return _mapper.Map<ICollection<ClienteDto>>(await _clienteRepository.ObterPorNome(nome));
        }

        public async Task<ValidationResult> Registrar(RegistrarClienteDto clienteViewModel)
        {
            var command = _mapper.Map<RegistrarClienteCommand>(clienteViewModel);
            return await _mediator.EnviarComando(command);
        }

        public async Task<ValidationResult> Remover(Guid id)
        {
            var command = new RemoverClienteCommand(id);
            return await _mediator.EnviarComando(command);
        }
    
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
