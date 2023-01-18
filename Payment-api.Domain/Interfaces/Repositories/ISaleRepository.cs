using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Interfaces.Repositories
{
    public interface ISaleRepository : IBaseRepository<SaleEntity>
    {
        Task<SaleEntity>GetByOrder(Guid OrderId);
    }
}
