using ForDevs.Domain.Core.DomainObjects;

namespace ForDevs.Domain.Core.Data
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
