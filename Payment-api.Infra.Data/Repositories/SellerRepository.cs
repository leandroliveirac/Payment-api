using Microsoft.EntityFrameworkCore;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Infra.Data.Context;

namespace Payment_api.Infra.Data.Repositories
{
    public sealed class SellerRepository : BaseRepository<ApplicationDbContext, SellerEntity>, ISellerRepository
    {
        public SellerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<SellerEntity> GetByIdAsync(Guid id)
        {
            return await _context.Sellers.Include(x => x.Phones).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
