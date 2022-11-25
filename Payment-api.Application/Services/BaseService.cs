using Payment_api.Application.Interfaces.Services;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Interfaces.UnitOfWork;

namespace Payment_api.Application.Services
{
    public abstract class BaseService<Entity> : IBaseService<Entity> 
        where Entity : BaseEntity
    {
        private readonly IBaseRepository<Entity> _baseRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BaseService(IBaseRepository<Entity> baseRepository, IUnitOfWork unitOfWork)
        {
            _baseRepository = baseRepository;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public virtual async Task CreateAsync(Entity entity)
        {
            await _baseRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public virtual void Remove(Entity entity)
        {
            _baseRepository.Remove(entity);
            _unitOfWork.Commit();
        }

        public virtual void Update(Entity entity)
        {
            _baseRepository.Update(entity);
            _unitOfWork.Commit();
        }
    }
}