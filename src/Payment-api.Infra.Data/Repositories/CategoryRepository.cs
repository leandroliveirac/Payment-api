using Microsoft.EntityFrameworkCore;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Infra.Data.Context;

namespace Payment_api.Infra.Data.Repositories
{
    public sealed class CategoryRepository : BaseRepository<ApplicationDbContext, CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CategoryEntity> GetByDescriptionAsync(string description)
        {
           return await _context.Categories.AsNoTracking()
                                        .FirstOrDefaultAsync(c => description.ToLower().Contains(c.Description.ToLower()));
        }
    }
}