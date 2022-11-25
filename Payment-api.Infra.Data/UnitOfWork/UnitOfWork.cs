using Microsoft.EntityFrameworkCore;
using Payment_api.Domain.Interfaces.UnitOfWork;

namespace Payment_api.Infra.Data.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}