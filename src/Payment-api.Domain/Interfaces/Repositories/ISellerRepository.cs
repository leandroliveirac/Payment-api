using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Interfaces.Repositories
{
    public interface ISellerRepository : IBaseRepository<SellerEntity>
    {
        Task<SellerEntity> GetByCpf(string cpf);
        Task<bool> ThereIsEmail(string email, Guid idSeller);
        Task<bool>HasSeller(Guid id);
    }
}
