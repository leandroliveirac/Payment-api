using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Interfaces.Services
{
    public interface ISellerService
    {
        Task<IEnumerable<SellerEntity>> GetAllAsync();
        Task<SellerEntity> GetByIdAsync(Guid id);
        Task<SellerEntity> CreateAsync(SellerEntity entity);
        SellerEntity Update(SellerEntity currentSeller,SellerEntity newSeller);
        void Remove(SellerEntity entity);
    }
}