using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Validation;

namespace Payment_api.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryAppService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<CategoryViewModel> GetByDescriptionAsync(string description)
        {
            var category = await _categoryRepository.GetByDescriptionAsync(description);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<CategoryViewModel> GetByIdAsync(Guid id)
        {
            IsValid(id);
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<CategoryViewModel> CreateAsync(CategoryInputModel entity)
        {    
            DomainExceptionValidation.When(await _categoryRepository.GetByDescriptionAsync(entity.Description) != null, "There is category with this description!");
            
            var category = _mapper.Map<CategoryEntity>(entity);                 

            await _categoryRepository.CreateAsync(category);

            return _mapper.Map<CategoryViewModel>(category);
        }

        public void Remove(Guid id)
        {
            IsValid(id);
            var category =  _categoryRepository.GetByIdAsync(id).Result;

            DomainExceptionValidation.When(category == null,"Not found");
            
            _categoryRepository.Remove(category);
        }

        public CategoryViewModel Update(Guid id, CategoryInputModel entity)
        {
            IsValid(id);
            var category = _categoryRepository.GetByIdAsync(id).Result;

            DomainExceptionValidation.When(category == null, "Not found");

            category.Update(entity.Description);

            _categoryRepository.Update(category);

            return _mapper.Map<CategoryViewModel>(category);
        }
        private void IsValid(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty, "Invalid argument");
        }
    }
}
