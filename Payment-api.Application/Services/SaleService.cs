using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Enums;
using Payment_api.Domain.Interfaces.Repositories;

namespace Payment_api.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<SaleViewModel>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SaleViewModel>>(sales);
        }

        public async Task<SaleViewModel> GetByIdAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            return _mapper.Map<SaleViewModel>(sale);
        }

        public async Task<SaleViewModel> CreateAsync(SaleInputModel entity)
        {
            if(entity.OrderId == Guid.Empty || entity.SellerId == Guid.Empty)
                throw new ArgumentException();            

            var order = await _orderRepository.GetByIdAsync(entity.OrderId);
            
            if(order.Status != OrderStatus.PROCESSING)
                throw new ArgumentException();

            order.UpdateStatus(OrderStatus.AWAITING_PAYMENT);
            _orderRepository.Update(order);
            
            var sale = _mapper.Map<SaleEntity>(entity);
            await _saleRepository.CreateAsync(sale);

            return _mapper.Map<SaleViewModel>(sale);
        }

        public void Remove(Guid id)
        {
            var sale = _saleRepository.GetByIdAsync(id);
            
            if(sale == null)
                throw new NullReferenceException();

            _saleRepository.Remove(sale.Result);
        }

        public void UpdateStatus(Guid id, string status)
        {
            var sale = _saleRepository.GetByIdAsync(id).Result;
            if(sale == null)
                throw new NullReferenceException();
            
            var _status =  Enum.Parse<SaleStatus>(status);

            sale.UpdateStatus(_status);
            _saleRepository.Update(sale);
        }

        public void Canceled(Guid id)
        {
            var sale = _saleRepository.GetByIdAsync(id).Result;
            if(sale == null)
                throw new NullReferenceException();    

            sale.Order.UpdateStatus(OrderStatus.CANCELED);

            sale.UpdateStatus(SaleStatus.CANCELED);
            _saleRepository.Update(sale);
        }

        public void PaymentAccept(Guid id)
        {
            var sale = _saleRepository.GetByIdAsync(id).Result;
            if(sale == null)
                throw new NullReferenceException();
            
            sale.Order.UpdateStatus(OrderStatus.PAYMENT_ACCEPT);

            sale.UpdateStatus(SaleStatus.PAYMENT_ACCEPT);
            _saleRepository.Update(sale);
        }

        public void SentCarrier(Guid id)
        {
            var sale = _saleRepository.GetByIdAsync(id).Result;
            if(sale == null)
                throw new NullReferenceException();
            
            sale.Order.UpdateStatus(OrderStatus.SENT);

            sale.UpdateStatus(SaleStatus.SENT_CARRIER);
            _saleRepository.Update(sale);
        }

        public void Delivered(Guid id)
        {
            var sale = _saleRepository.GetByIdAsync(id).Result;
            if(sale == null)
                throw new NullReferenceException();
            
            sale.Order.UpdateStatus(OrderStatus.DELIVERED);

            sale.UpdateStatus(SaleStatus.DELIVERED);
            _saleRepository.Update(sale);
        }

    }
}