using Payment_api.Application.Extensions.Mappings;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Validation;

namespace Payment_api.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryAppService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.MapParaListCategoryViewModel();
        }

        public async Task<CategoryViewModel> GetByDescriptionAsync(string description)
        {
            var category = await _categoryRepository.GetByDescriptionAsync(description);
            return category.MapParaCategoryViewModel();
        }

        public async Task<CategoryViewModel> GetByIdAsync(Guid id)
        {
            IsValid(id);
            var category = await _categoryRepository.GetByIdAsync(id);
            return category.MapParaCategoryViewModel();
        }

        public async Task<CategoryViewModel> CreateAsync(CategoryInputModel entity)
        {    
            DomainExceptionValidation.When(await _categoryRepository.GetByDescriptionAsync(entity.Description) != null, "There is category with this description!");
            
            var category = entity.MapParaCategoryEntity();                 

            await _categoryRepository.CreateAsync(category);

            return category.MapParaCategoryViewModel();
        }

        public async Task RemoveAsync(Guid id)
        {
            IsValid(id);
            var category = await  _categoryRepository.GetByIdAsync(id);

            DomainExceptionValidation.When(category == null,"Not found");
            
            _categoryRepository.Remove(category);
        }

        public async Task<CategoryViewModel> UpdateAsync(Guid id, CategoryInputModel entity)
        {
            IsValid(id);
            var category = await _categoryRepository.GetByIdAsync(id);

            DomainExceptionValidation.When(category == null, "Not found");

            category.Update(entity.Description);

            _categoryRepository.Update(category);

            return category.MapParaCategoryViewModel();
        }
        private void IsValid(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty, "Invalid argument");
        }
    }
}
