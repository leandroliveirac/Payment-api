using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;

namespace Payment_api.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
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
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<CategoryViewModel> CreateAsync(CategoryInputModel entity)
        {
            var category = _mapper.Map<CategoryEntity>(entity);
            await _categoryRepository.CreateAsync(category);

            return _mapper.Map<CategoryViewModel>(category);
        }

        public void Remove(CategoryInputModel entity)
        {
            var category =  _categoryRepository.GetByDescriptionAsync(entity.Description).Result;
            if (category == null)
                throw new NullReferenceException();
            
            _categoryRepository.Remove(category);
        }

        public CategoryViewModel Update(Guid id, string newDescription)
        {
            var category = _categoryRepository.GetByIdAsync(id).Result;
            if (category == null)
                throw new NullReferenceException();

            category.Update(newDescription);
            _categoryRepository.Update(category);

            return _mapper.Map<CategoryViewModel>(category);
        }
    }
}
