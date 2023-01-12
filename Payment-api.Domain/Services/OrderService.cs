using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Services.Interfaces;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderEntity>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<OrderEntity> GetByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }        

        public async Task<OrderEntity> CreateAsync(OrderEntity entity)
        {           
            Validate(entity);

            await _orderRepository.CreateAsync(entity);
            return entity;
        }

        public void Canceled(Guid id)
        {
            var order = _orderRepository.GetByIdAsync(id).Result;

            DomainExceptionValidation.When(order == null,"Not found");   

            order?.UpdateStatus(Domain.Enums.OrderStatus.CANCELED);

            _orderRepository.Update(order);
        }       

        public void Remove(OrderEntity entity, Guid id)
        {

            var order = _orderRepository.GetByIdAsync(id).Result;

            DomainExceptionValidation.When(order == null,"Not found");            

            DomainExceptionValidation.When(entity.Date != order?.Date,"Doesn´t check");

            _orderRepository.Remove(entity);

        }
        private void Validate(OrderEntity order)
        {
            DomainExceptionValidation.When(order.Items == null || order.Items.Count() <= 0,"Enter at least one item to order");
            foreach (var item in order.Items)
            {            
                DomainExceptionValidation.When(item.ProductId == Guid.Empty,"");
            }
        }
    }
}