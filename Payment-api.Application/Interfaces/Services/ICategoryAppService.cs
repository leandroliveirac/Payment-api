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
        CategoryViewModel Update(Guid id, CategoryInputModel entity);
        void Remove(Guid id);

    }
}