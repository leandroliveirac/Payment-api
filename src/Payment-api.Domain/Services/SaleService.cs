using Payment_api.Domain.Entities;
using Payment_api.Domain.Enums;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Interfaces.Services;
using Payment_api.Domain.Validation;

namespace Payment_api.Domain.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISellerRepository _sellerRepository;

        public SaleService(ISaleRepository saleRepository, IOrderRepository orderRepository, ISellerRepository sellerRepository)
        {
            _saleRepository = saleRepository;
            _orderRepository = orderRepository;
            _sellerRepository = sellerRepository;
        }

        public async Task<IEnumerable<SaleEntity>> GetAllAsync()
        {
            return await _saleRepository.GetAllAsync();
        }

        public async Task<SaleEntity> GetByIdAsync(Guid id)
        {
            return await _saleRepository.GetByIdAsync(id);
        }        

        public async Task<SaleEntity> CreateAsync(SaleEntity entity)
        {
            DomainExceptionValidation.When(entity.OrderId == Guid.Empty || entity.SellerId == Guid.Empty, "Invalid argument. Enter in Order and Seller");

            var order = await _orderRepository.GetByIdAsync(entity.OrderId);
            DomainExceptionValidation.When(order == null, "Order entered not found, Enter in Order");
            
            var seller = await _sellerRepository.GetByIdAsync(entity.SellerId);
            DomainExceptionValidation.When(seller == null, "Seller entered not found, Enter in Seller");
            
            DomainExceptionValidation.When(order?.Status != OrderStatus.PROCESSING,"Order not available for sale");

            order.UpdateStatus(OrderStatus.AWAITING_PAYMENT);
            _orderRepository.Update(order);
           
            await _saleRepository.CreateAsync(entity);
            entity.Seller = seller;
            return entity;
        }       

        public void UpdateStatus(Guid id, SaleStatus status)
        {
            var sale = GetByIdAsync(id).Result;
            IsNull(sale);

            sale?.UpdateStatus(status);
            SetNull(sale);
            _saleRepository.Update(sale);
        }
        public void Canceled(Guid id)
        {
            var sale = GetByIdAsync(id).Result;
            
            IsNull(sale);

            sale?.Order?.UpdateStatus(OrderStatus.CANCELED);

            sale?.UpdateStatus(SaleStatus.CANCELED);
            SetNull(sale);
            _saleRepository.Update(sale);
        }

         public void Delivered(Guid id)
        {
            var sale = GetByIdAsync(id).Result;
            IsNull(sale);

            sale?.Order?.UpdateStatus(OrderStatus.DELIVERED);

            sale?.UpdateStatus(SaleStatus.DELIVERED);
            SetNull(sale);
            _saleRepository.Update(sale);
        }        

        public void PaymentAccept(Guid id)
        {
            var sale = GetByIdAsync(id).Result;
            IsNull(sale);

            sale?.Order?.UpdateStatus(OrderStatus.PAYMENT_ACCEPT);

            sale?.UpdateStatus(SaleStatus.PAYMENT_ACCEPT);
            SetNull(sale);
            _saleRepository.Update(sale);
        }       

        public void SentCarrier(Guid id)
        {
            var sale = GetByIdAsync(id).Result;
            
            IsNull(sale);

            sale.Order?.UpdateStatus(OrderStatus.SENT);

            sale?.UpdateStatus(SaleStatus.SENT_CARRIER);
            SetNull(sale);
            _saleRepository.Update(sale);
        }

        private void IsNull(SaleEntity entity)
        {
            DomainExceptionValidation.When(entity == null, "Not found");
        }
        private static void SetNull(SaleEntity sale)
        {
            foreach (var item in sale.Order.Items)
            {
                item.Product = null;
            }
        }
    }
}