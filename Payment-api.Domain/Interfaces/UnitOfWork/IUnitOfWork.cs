namespace Payment_api.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}