using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;

namespace Payment_api.Application.Interfaces.Services
{
    public interface ICategoryAppService 
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel> GetByIdAsync(Guid id);
        Task<CategoryViewModel> GetByDescriptionAsync(string description);
        Task<CategoryViewModel> CreateAsync(CategoryInputModel entity);
        Task<CategoryViewModel> UpdateAsync(Guid id, CategoryInputModel entity);
        Task RemoveAsync(Guid id);

    }
}