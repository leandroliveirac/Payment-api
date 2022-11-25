using Microsoft.EntityFrameworkCore;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Infra.Data.Context;

namespace Payment_api.Infra.Data.Repositories
{
    public sealed class OrderRepository : BaseRepository<ApplicationDbContext, OrderEntity>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<OrderEntity>> GetAllAsync()
        {
            return await _context.Orders.Include(x => x.Items)
                                            .ThenInclude(i => i.Product)
                                                .ThenInclude(p => p.Category)
                                        .ToListAsync();
        }

        public override async Task<OrderEntity> GetByIdAsync(Guid id)
        {
            return await _context.Orders.Include(x => x.Items)
                                        .ThenInclude(i => i.Product)
                                                .ThenInclude(p => p.Category)
                                        .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
