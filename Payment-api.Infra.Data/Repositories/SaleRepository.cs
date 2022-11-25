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
    }
}
