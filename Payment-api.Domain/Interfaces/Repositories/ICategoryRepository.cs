using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<CategoryEntity>
    {
        Task<CategoryEntity> GetByDescriptionAsync(string description);
    }
}