using ForDevs.Domain.Core.Data;
using ForDevs.Domain.Models;

namespace ForDevs.Domain.Interfaces
{
    public interface IAvaliacaoRepository : IRepository<Avaliacao>
    {
        Task<Avaliacao> ObterPorId(Guid id);
        Task<AvaliacaoCliente> ObterUltimaAvaliacao();
        Task<ICollection<Avaliacao>> ObterPorLista();
        void Adicionar(Avaliacao avaliacao);
        void Atualizar(Avaliacao avaliacao);
        void Remover(Avaliacao avaliacao);
        void Remover(ICollection<AvaliacaoCliente> avaliacaoClientes);
        void RemoverAvalicoesClientes(ICollection<AvaliacaoCliente> avaliacaoClientes);
        void AdicionarAvalicoesClientes(ICollection<AvaliacaoCliente> avaliacaoClientes);
    }
}
