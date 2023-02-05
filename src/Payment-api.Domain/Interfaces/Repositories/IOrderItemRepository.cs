using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Interfaces.Repositories
{
    public interface IOrderItemRepository : IBaseRepository<OrderItemEntity>
    {
        Task CreateRangeAsync(IEnumerable<OrderItemEntity> orderItems);
    }
}
