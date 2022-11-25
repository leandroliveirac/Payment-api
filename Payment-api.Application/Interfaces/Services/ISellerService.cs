using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;

namespace Payment_api.Application.Interfaces.Services
{
    public interface ISellerService
    {
        Task<IEnumerable<SellerViewModel>> GetAllAsync();
        Task<SellerViewModel> GetByIdAsync(Guid id);
        Task<SellerViewModel> CreateAsync(SellerInputModel entity);
        SellerViewModel Update(SellerInputModel entity, Guid id);
        void Remove(Guid id);
    }
}