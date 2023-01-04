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

        public async Task<bool> ThereIsEmail(string email, Guid idSeller)
        {
            return await _context.Sellers.AsNoTracking()
                                        .AnyAsync(s => s.Email == email && s.Id != idSeller);
        }

        public async Task<SellerEntity> GetByCpf(string cpf)
        {
            return await _context.Sellers.AsNoTracking()
                                    .FirstOrDefaultAsync(s => s.Cpf == cpf);
        }

        public override async Task<IEnumerable<SellerEntity>> GetAllAsync()
        {
            return await _context.Sellers.AsNoTracking()
                                        .Include(s => s.Phones)
                                        .ToListAsync();
        }

        public override async Task<SellerEntity> GetByIdAsync(Guid id)
        {
            return await _context.Sellers.Include(x => x.Phones).AsNoTracking()
                                                                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
