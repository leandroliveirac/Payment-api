using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderEntity>> GetAllAsync();
        Task<OrderEntity> GetByIdAsync(Guid id);
        Task<OrderEntity> CreateAsync(OrderEntity entity);
        void Canceled(Guid id);
        void Remove(OrderEntity entity, Guid id);
    }
}