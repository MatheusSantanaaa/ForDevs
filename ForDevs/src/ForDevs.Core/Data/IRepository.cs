using ForDevs.Core.DomainObjects;

namespace ForDevs.Core.Data
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
