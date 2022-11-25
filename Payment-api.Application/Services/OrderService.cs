using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;

namespace Payment_api.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
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
                var order = _mapper.Map<OrderEntity>(entity);

                await _orderRepository.CreateAsync(order);

                return _mapper.Map<OrderViewModel>(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(OrderInputModel entity, Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid argument");

            var order = _orderRepository.GetByIdAsync(id).Result;

            if (order == null)
                throw new NullReferenceException();

            _orderRepository.Remove(order);
        }     

        public void Canceled(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid argument");

            var order = _orderRepository.GetByIdAsync(id).Result;

            if (order == null)
                throw new NullReferenceException();

            order.UpdateStatus(Domain.Enums.OrderStatus.CANCELED);

            _orderRepository.Update(order);
        }
    }
}
