using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> GetByDescriptionAsync(string description);
    }
}
