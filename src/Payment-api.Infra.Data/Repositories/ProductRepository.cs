using Microsoft.EntityFrameworkCore;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Infra.Data.Context;

namespace Payment_api.Infra.Data.Repositories
{
    public sealed class ProductRepository : BaseRepository<ApplicationDbContext, ProductEntity>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _context.Products
                                        .AsNoTracking()
                                        .Include(x => x.Category)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetByDescriptionAsync(string description)
        {
            return await _context.Products
                                        .AsNoTracking()
                                        .Include(p => p.Category)
                                        .Where(p => p.Description.ToLower().Contains(description.ToLower().Trim()))
                                        .ToListAsync();
        }
        public override async Task<ProductEntity> GetByIdAsync(Guid id)
        {
            return await _context.Products
                                        .AsNoTracking()
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductEntity>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Products
                                        .AsNoTracking()
                                        .Include(p => p.Category)
                                        .Where(p => ids.Contains(p.Id))
                                        .ToListAsync();
        }

    }
}