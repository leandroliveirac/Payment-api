using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;

namespace Payment_api.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> CreateAsync(ProductInputModel entity)
        {
            try
            {
                var product = _mapper.Map<ProductEntity>(entity);
                await _productRepository.CreateAsync(product);

                return _mapper.Map<ProductViewModel>(product);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var product = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductViewModel>>(product);
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<IEnumerable<ProductViewModel>> GetByDescriptionAsync(string description)
        {
            var product = await _productRepository.GetByDescriptionAsync(description);
            return _mapper.Map<IEnumerable<ProductViewModel>>(product);
        }

        public void Remove(Guid productId)
        {
            try
            {
                var product = _productRepository.GetByIdAsync(productId).Result;
                if (product == null)
                    throw new NullReferenceException();

                _productRepository.Remove(product);
            }
            catch
            {

                throw;
            }
        }

        public ProductViewModel Update(ProductInputModel entity, Guid productId)
        {

            try
            {
                if (productId == Guid.Empty)
                    throw new ArgumentException("Invalid argument.");

                var product = _productRepository.GetByIdAsync(productId).Result;
                if (product == null)
                    throw new NullReferenceException();

                product.Update(entity.Description, entity.Price, entity.CategoryId);
                _productRepository.Update(product);

                return _mapper.Map<ProductViewModel>(product);
            }
            catch
            {
                throw;
            }
        }
    }
}