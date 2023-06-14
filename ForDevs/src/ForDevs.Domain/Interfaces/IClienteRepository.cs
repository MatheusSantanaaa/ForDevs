using ForDevs.Domain.Core.Data;
using ForDevs.Domain.Models;

namespace ForDevs.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterPorId(Guid id);
        Task<Cliente> ObterPorCnpj(string cnpj);
        Task<ICollection<Cliente>> ObterPorListaDeId(IEnumerable<Guid> ids);
        Task<ICollection<Cliente>> ObterLista();
        void Adicionar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Remover(Cliente cliente);
        Task<ICollection<Cliente>> ObterPorNome(string nome);
        Task<Cliente> ObterPorIdParaRemover(Guid id);
        Task<ICollection<Cliente>> ObterClientesParaAtualizarCategoria(List<Guid> id);
    }
}
