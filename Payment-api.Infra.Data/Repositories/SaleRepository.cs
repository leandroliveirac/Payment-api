using Microsoft.EntityFrameworkCore;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Infra.Data.Context;

namespace Payment_api.Infra.Data.Repositories
{
    public sealed class SaleRepository : BaseRepository<ApplicationDbContext, SaleEntity>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<SaleEntity> GetByIdAsync(Guid id)
        {
            return _context.Sales
                            .Include(x => x.Order)
                                .ThenInclude(o => o.Items)
                                    .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.Category)
                            .Include(x => x.Seller)
                                .ThenInclude(s => s.Phones)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
