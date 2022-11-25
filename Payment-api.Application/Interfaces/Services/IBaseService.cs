namespace Payment_api.Application.Interfaces.Services
{
    public interface IBaseService<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(Guid id);
        Task CreateAsync(Entity entity);
        void Update(Entity entity);
        void Remove(Entity entity);
    }
}