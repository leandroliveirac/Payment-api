using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Services.Interfaces;
using Payment_api.Domain.Validation;

namespace Payment_api.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderAppService(IMapper mapper, IOrderService orderService, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _orderService = orderService;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync()
        {
            var order = await _orderRepository.GetAllAsync();

            if (order == null)
                throw new NullReferenceException();

            return _mapper.Map<IEnumerable<OrderViewModel>>(order);
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new NullReferenceException();

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderViewModel> CreateAsync(OrderInputModel entity)
        {
            try
            {
                var order = _mapper.Map<OrderInputModel,OrderEntity>(entity, opt => {
                    opt.BeforeMap((src,dest) => {
                        foreach (var item in src.Items)
                        {
                            item.OrderId = Guid.Empty;
                        }
                    });
                });

                await _orderService.CreateAsync(order);

                var ProductIds = order.Items.Select(x => x.ProductId);
                var products = await _productRepository.GetByIdsAsync(ProductIds);
                
                foreach (var item in order.Items)
                {
                    item.Product =  products.FirstOrDefault(x => x.Id == item.ProductId);
                }

                return _mapper.Map<OrderViewModel>(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Canceled(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty,"Invalid argument");

            _orderService.GetByIdAsync(id);
        }
    }
}
