using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Interfaces.Services;
using Payment_api.Domain.Validation;

namespace Payment_api.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;        
        private readonly IMapper _mapper;

        public OrderAppService(IMapper mapper, IOrderService orderService, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync()
        {
            var order = await _orderRepository.GetAllAsync();
                      
            return _mapper.Map<IEnumerable<OrderViewModel>>(order);
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id)
        {
            IsValid(id);

            var order = await _orderRepository.GetByIdAsync(id);
            
            DomainExceptionValidation.When(order == null, "Not found");

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderViewModel> CreateAsync(OrderInputModel entity)
        {
            var order = _mapper.Map<OrderInputModel,OrderEntity>(entity);

            await _orderService.CreateAsync(order);                

            return _mapper.Map<OrderEntity,OrderViewModel>(order);
        }

        public void Canceled(Guid id)
        {
            IsValid(id);

            _orderService.Canceled(id);
        }
        

        public void Returned(Guid id)
        {
            IsValid(id);

            _orderService.Returned(id);
        }

        public void Sent(Guid id)
        {
            IsValid(id);

            _orderService.Sent(id);
        }

        public void Delivered(Guid id)
        {
            IsValid(id);

            _orderService.Delivered(id);
        }

        private void IsValid(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty,"Invalid argument");
        }        
    }
}
