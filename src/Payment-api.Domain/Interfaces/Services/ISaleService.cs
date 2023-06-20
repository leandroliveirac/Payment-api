using Payment_api.Domain.Entities;
using Payment_api.Domain.Enums;

namespace Payment_api.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleEntity>> GetAllAsync();
        Task<SaleEntity> GetByIdAsync(Guid id);
        Task<SaleEntity> CreateAsync(SaleEntity entity);
        void UpdateStatus(Guid id, SaleStatus status);
        void Canceled(Guid id);
        void PaymentAccept(Guid id);
        void SentCarrier(Guid id);
        void Delivered(Guid id);
    }
}