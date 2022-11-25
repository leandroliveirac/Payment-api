using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Enums;

namespace Payment_api.Application.Interfaces.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleViewModel>> GetAllAsync();
        Task<SaleViewModel> GetByIdAsync(Guid id);
        Task CreateAsync(SaleInputModel entity);
        void UpdateStatus(Guid saleId, SaleStatus status);
        void Remove(SaleInputModel entity,Guid saleId);
    }
}