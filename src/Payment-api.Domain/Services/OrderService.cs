using Payment_api.Domain.Entities;
using Payment_api.Domain.Enums;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Interfaces.Services;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;

        private readonly ISaleService _saleService;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ISaleRepository saleRepository, ISaleService saleService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _saleService = saleService;
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

            var ProductIds = entity.Items.Select(x => x.ProductId);
            var products = await _productRepository.GetByIdsAsync(ProductIds);           

            DomainExceptionValidation.When(products.Count() < ProductIds.Count(),"Products entered are not valid");            
            bool hasInactiveProduct = products.Any(p => p.Active == false);
            DomainExceptionValidation.When(hasInactiveProduct, "There is inactive product");

            entity.Items = UpdateOrderItems(entity.Items.ToList(),products);

            await _orderRepository.CreateAsync(entity);        

            foreach (var item in entity.Items)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                item.Product =  product;
            }
                 
            return entity;
            
        }

        public void Canceled(Guid id)
        {
            var order = _orderRepository.GetByIdAsync(id).Result;            
            DomainExceptionValidation.When(order == null,"Not found");
            
            var sale = _saleRepository.GetByOrder(id).Result;
            DomainExceptionValidation.When(sale != null && sale.Status != SaleStatus.AWAITING_PAYMENT, "There is an active sales transaction. Cancel the sale");            
            
            _saleService.Canceled(sale.Id);
        }        

        public void Returned(Guid id)
        {
            var order = _orderRepository.GetByIdAsync(id).Result;            
            DomainExceptionValidation.When(order == null,"Not found");
            
            var sale = _saleRepository.GetByOrder(id).Result;
            DomainExceptionValidation.When(sale != null && sale.Status != SaleStatus.DELIVERED, "There is an active sales transaction. Cancel the sale");
            
            sale.UpdateStatus(SaleStatus.PAYMENT_ACCEPT);
            _saleRepository.Update(sale);

            order?.UpdateStatus(Domain.Enums.OrderStatus.RETURNED);

            _orderRepository.Update(order);
        }  
        
        public void Sent(Guid id)
        {
            var order = _orderRepository.GetByIdAsync(id).Result;            
            DomainExceptionValidation.When(order == null,"Not found");           
            
            var sale = _saleRepository.GetByOrder(id).Result;
            
            DomainExceptionValidation.When(sale != null && (sale.Status != SaleStatus.SENT_CARRIER && 
                                                            sale.Status != SaleStatus.PAYMENT_ACCEPT), 
                                                            "The order can only be shipped when the sale status is payment approved");
            _saleService.SentCarrier(sale.Id);          
        }

        public void Delivered(Guid id)
        {
            var order = _orderRepository.GetByIdAsync(id).Result;            
            DomainExceptionValidation.When(order == null,"Not found");           
            
            var sale = _saleRepository.GetByOrder(id).Result;
            
            DomainExceptionValidation.When(sale != null && sale.Status != SaleStatus.SENT_CARRIER, "Check the status of the sale");
            _saleService.Delivered(sale.Id);
        }

        public void Remove(OrderEntity entity, Guid id)
        {

            var order = _orderRepository.GetByIdAsync(id).Result;

            DomainExceptionValidation.When(order == null,"Not found");            

            DomainExceptionValidation.When(entity.Date != order?.Date,"Doesn´t check");

            var sale = _saleRepository.GetByOrder(id).Result;
            DomainExceptionValidation.When(sale != null, "There is an active sales transaction. Remove the sale");

            _orderRepository.Remove(entity);
        }


        private void Validate(OrderEntity order)
        {
            DomainExceptionValidation.When(order.Items == null || order.Items.Count() <= 0,"Enter at least one item to order");
            DomainExceptionValidation.When(order.Items.Any(o => o.ProductId == Guid.Empty),"Product is required");
            DomainExceptionValidation.When(order.Items.Any(o => o.ProductId == Guid.Empty),"Product is required");
        } 

        private List<OrderItemEntity> UpdateOrderItems(List<OrderItemEntity> orderItems, IEnumerable<ProductEntity> products)
        {
            orderItems.ForEach(o => {
                var product = products.FirstOrDefault(p => p.Id == o.ProductId);
                o.Update(o.OrderId,o.ProductId,product.Description,product.Price,o.Quantity);
            });
            return orderItems;
        }       
    }
}