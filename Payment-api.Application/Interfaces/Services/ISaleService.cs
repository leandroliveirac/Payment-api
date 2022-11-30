using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Enums;

namespace Payment_api.Application.Interfaces.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleViewModel>> GetAllAsync();
        Task<SaleViewModel> GetByIdAsync(Guid id);
        Task<SaleViewModel> CreateAsync(SaleInputModel entity);
        void UpdateStatus(Guid id, string status);
        void Remove(Guid id);
        void Canceled(Guid id);
        void PaymentAccept(Guid id);
        void SentCarrier(Guid id);
        void Delivered(Guid id);
    }
}