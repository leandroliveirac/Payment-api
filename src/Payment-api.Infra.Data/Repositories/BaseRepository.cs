using Microsoft.EntityFrameworkCore;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;

namespace Payment_api.Infra.Data.Repositories
{
    public abstract class BaseRepository<TContext, Entity> : IBaseRepository<Entity>
        where TContext : DbContext
        where Entity : BaseEntity
    {

        protected readonly TContext _context;
        public BaseRepository(TContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _context.Set<Entity>()
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(Guid id)
        {
            return await _context.Set<Entity>()
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public virtual async Task CreateAsync(Entity entity)
        {
            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void Remove(Entity entity)
        {
            _context.Set<Entity>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Update(Entity entity)
        {
            _context.Set<Entity>().Update(entity);

            _context.SaveChanges();
        }
    }
}