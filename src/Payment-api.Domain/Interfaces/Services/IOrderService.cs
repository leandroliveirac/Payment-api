using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderEntity>> GetAllAsync();
        Task<OrderEntity> GetByIdAsync(Guid id);
        Task<OrderEntity> CreateAsync(OrderEntity entity);
        void Canceled(Guid id);
        void Returned(Guid id);
        void Sent(Guid id);
        void Delivered(Guid id);
        void Remove(OrderEntity entity, Guid id);
    }
}