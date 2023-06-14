using FluentValidation.Results;
using ForDevs.Application.Dtos.Avaliacao;

namespace ForDevs.Application.Interfaces
{
    public interface IAvaliacaoAppService : IDisposable
    {
        Task<AvaliacaoDto> ObterPorId(Guid id);
        Task<ICollection<AvaliacaoDto>> ObterLista();
        Task<ValidationResult> Registrar(RegistrarAvaliacaoDto avaliacaoViewModel);
        Task<ValidationResult> Atualizar(AtualizarAvaliacaoDto avaliacaoViewModel);
        Task<ValidationResult> Remover(Guid id);
    }
}
