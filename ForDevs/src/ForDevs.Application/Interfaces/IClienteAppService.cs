using FluentValidation.Results;
using ForDevs.Application.Dtos.Cliente;

namespace ForDevs.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        Task<ClienteDto> ObterPorId(Guid id);
        Task<ICollection<ClienteDto>> ObterLista();
        Task<ValidationResult> Registrar(RegistrarClienteDto clienteViewModel);
        Task<ValidationResult> Atualizar(AtualizarClienteDto clienteViewModel);
        Task<ValidationResult> Remover(Guid id);
        Task<ICollection<ClienteDto>> ObterPorNome(string nome);
    }
}
