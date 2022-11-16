namespace Payment_api.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(Guid id);
        Task InsertAsync(Entity entity);
        Task UpdateAsync(Entity entity);
        Task RemoveAsync(Entity entity);
    }
}