using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Infra.Data.Context;

namespace Payment_api.Infra.Data.Repositories
{
    public sealed class PhoneRepository : BaseRepository<ApplicationDbContext, PhoneEntity>, IPhoneRepository
    {
        public PhoneRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void RemoveRange(IEnumerable<PhoneEntity> entities)
        {
            _context.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task CreateRangeAsync(IEnumerable<PhoneEntity> entities)
        {
            await _context.Phones.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
