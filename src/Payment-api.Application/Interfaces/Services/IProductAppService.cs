using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;

namespace Payment_api.Application.Interfaces.Services
{
    public interface IProductAppService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductViewModel>> GetByDescriptionAsync(string description);
        Task<ProductViewModel> CreateAsync(ProductInputModel entity);
        ProductViewModel Update(ProductInputModel entity, Guid id);
        void Inactivate(Guid productId);
    }
}