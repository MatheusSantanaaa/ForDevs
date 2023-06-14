using ForDevs.Domain.Core.Data;
using ForDevs.Domain.Interfaces;
using ForDevs.Domain.Models;
using ForDevs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ForDevs.Infra.Data.Repository
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        protected readonly ForDevsContext Db;
        protected readonly DbSet<Avaliacao> DbSet;

        public AvaliacaoRepository(ForDevsContext context)
        {
            Db = context;
            DbSet = Db.Set<Avaliacao>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public void Adicionar(Avaliacao avaliacao)
        {
            DbSet.Add(avaliacao);
        }

        public void AdicionarAvalicoesClientes(ICollection<AvaliacaoCliente> avaliacaoClientes)
        {
            Db.AvaliacoesClientes.AddRange(avaliacaoClientes);
        }

        public void Atualizar(Avaliacao avaliacao)
        {
            DbSet.Update(avaliacao);
        }

        public async Task<Avaliacao> ObterPorId(Guid id)
        {
            return await DbSet
                        .Include(x => x.AvaliacaoClientes).ThenInclude(x => x.Cliente)
                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<ICollection<Avaliacao>> ObterPorLista()
        {

            return await DbSet
                        .AsNoTracking()
                        .Include(x => x.AvaliacaoClientes).ThenInclude(x => x.Cliente)
                        .ToListAsync();
        }

        public async Task<AvaliacaoCliente> ObterUltimaAvaliacao()
        {
            return await Db.AvaliacoesClientes
                        .AsNoTracking()
                        .OrderByDescending(x => x.DataDeCriacao)
                        .FirstOrDefaultAsync();
        }

        public void Remover(Avaliacao avaliacao)
        {
            DbSet.Remove(avaliacao);
        }

        public void Remover(ICollection<AvaliacaoCliente> avaliacaoClientes)
        {
            Db.AvaliacoesClientes.RemoveRange(avaliacaoClientes);
        }

        public void RemoverAvalicoesClientes(ICollection<AvaliacaoCliente> avaliacaoClientes)
        {
            Db.AvaliacoesClientes.RemoveRange(avaliacaoClientes);
        }
    }
}
