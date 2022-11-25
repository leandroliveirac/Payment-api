using Microsoft.EntityFrameworkCore;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Infra.Data.Context;
using SQLitePCL;

namespace Payment_api.Infra.Data.Repositories
{
    public sealed class CategoryRepository : BaseRepository<ApplicationDbContext, CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CategoryEntity> GetByDescriptionAsync(string description)
        {
           return await _context.Categories.FirstOrDefaultAsync(c => c.Description.ToLower() == description.ToLower());
        }
    }
}