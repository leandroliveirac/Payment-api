using Payment_api.Application.Extensions.Mappings;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Validation;

namespace Payment_api.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductAppService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductViewModel> CreateAsync(ProductInputModel entity)
        {
            var product = entity.MapParaProductEntity();
            await _productRepository.CreateAsync(product);

            product.Category = await _categoryRepository.GetByIdAsync(product.CategoryId);

            return product.MapParaProductViewModel();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var product = await _productRepository.GetAllAsync();
            return product.MapParaListProductViewModel();
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty,"Invalid argument.");

            var product = await _productRepository.GetByIdAsync(id);
            return product.MapParaProductViewModel();
        }

        public async Task<IEnumerable<ProductViewModel>> GetByDescriptionAsync(string description)
        {
            var product = await _productRepository.GetByDescriptionAsync(description);
            return product.MapParaListProductViewModel();
        }        

        public ProductViewModel Update(ProductInputModel entity, Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty, "Invalid argument.");

            var product = _productRepository.GetByIdAsync(id).Result;
            DomainExceptionValidation.When(product == null, "Product Not Found");

            product.Update(entity.Description, entity.Price, entity.CategoryId, entity.Active);
            _productRepository.Update(product);

            return product.MapParaProductViewModel();
        }

        public void Inactivate(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty,"Invalid argument.");
                
                var product = _productRepository.GetByIdAsync(id).Result;
                DomainExceptionValidation.When(product == null, "Product Not Found");

                product.Inactivate();
                _productRepository.Update(product);
        }
    }
}