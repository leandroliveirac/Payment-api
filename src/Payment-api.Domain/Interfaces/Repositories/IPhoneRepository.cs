using Payment_api.Domain.Entities;

namespace Payment_api.Domain.Interfaces.Repositories
{
    public interface IPhoneRepository : IBaseRepository<PhoneEntity>
    {
        void RemoveRange(IEnumerable<PhoneEntity> entities);
        Task CreateRangeAsync(IEnumerable<PhoneEntity> entities);
    }
}
