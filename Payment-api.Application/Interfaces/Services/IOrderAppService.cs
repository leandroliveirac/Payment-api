using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;

namespace Payment_api.Application.Interfaces.Services
{
    public interface IOrderAppService
    {
         Task<IEnumerable<OrderViewModel>> GetAllAsync();
        Task<OrderViewModel> GetByIdAsync(Guid id);
        Task<OrderViewModel> CreateAsync(OrderInputModel entity);
        void Canceled(Guid id);
    }
}