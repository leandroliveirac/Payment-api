namespace Payment_api.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(Guid id);
        Task CreateAsync(Entity entity);
        void Update(Entity entity);
        void Remove(Entity entity);
    }
}