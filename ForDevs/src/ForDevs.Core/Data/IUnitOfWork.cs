namespace ForDevs.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
