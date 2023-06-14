using ForDevs.Domain.Core.Data;
using ForDevs.Domain.Interfaces;
using ForDevs.Domain.Models;
using ForDevs.Infra.Data.Context;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ForDevs.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        protected readonly ForDevsContext Db;
        protected readonly DbSet<Cliente> DbSet;

        public ClienteRepository(ForDevsContext context)
        {
            Db = context;
            DbSet = Db.Set<Cliente>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public void Adicionar(Cliente cliente)
        {
            DbSet.Add(cliente);
        }

        public void Atualizar(Cliente cliente)
        {
            DbSet.Update(cliente);
        }

        public void Remover(Cliente cliente)
        {
            DbSet.Remove(cliente);
        }

        public async Task<ICollection<Cliente>> ObterLista()
        {
            return await DbSet
                        .Include(x => x.AvaliacaoClientes)
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            return await DbSet
                        .AsNoTracking()
                        .Include(x => x.AvaliacaoClientes)
                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Cliente> ObterPorIdParaRemover(Guid id)
        {
            return await DbSet
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Cliente> ObterPorCnpj(string cnpj)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Cnpj.Equals(cnpj));
        }

        public async Task<ICollection<Cliente>> ObterPorListaDeId(IEnumerable<Guid> ids)
        {
            return await DbSet
                        .AsNoTracking()
                        .Include(x => x.AvaliacaoClientes)
                        .Where(x => ids.Contains(x.Id))
                        .ToListAsync();
        }

        public async Task<ICollection<Cliente>> ObterPorNome(string nome)
        {
            var predicate = PredicateBuilder.New<Cliente>(true);
            predicate = predicate.Or(p => p.NomeContato.Contains(nome));
            predicate = predicate.Or(p => p.NomeDoCliente.Contains(nome));

            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<ICollection<Cliente>> ObterClientesParaAtualizarCategoria(List<Guid> clientes)
        {
            return await DbSet.Where(x => clientes.Contains(x.Id)).ToListAsync();
        }
    }
}
