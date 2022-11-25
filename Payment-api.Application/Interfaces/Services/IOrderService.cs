using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;

namespace Payment_api.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllAsync();
        Task<OrderViewModel> GetByIdAsync(Guid id);
        Task<OrderViewModel> CreateAsync(OrderInputModel entity);
        void Canceled(Guid id);
        void Remove(OrderInputModel entity, Guid id);
    }
}