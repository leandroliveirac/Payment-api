using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Enums;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Interfaces.Services;
using Payment_api.Domain.Validation;

namespace Payment_api.Application.Services
{
    public class SaleAppService : ISaleAppService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SaleAppService(ISaleRepository saleRepository, IMapper mapper, IOrderRepository orderRepository, ISaleService saleService)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _saleService = saleService;
        }

        public async Task<IEnumerable<SaleViewModel>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            
            return _mapper.Map<IEnumerable<SaleEntity>,IEnumerable<SaleViewModel>>(sales);
        }

        public async Task<SaleViewModel> GetByIdAsync(Guid id)
        {
            IsValid(id);

            var sale = await _saleRepository.GetByIdAsync(id);
            
            DomainExceptionValidation.When(sale == null, "Not found");

            return _mapper.Map<SaleEntity,SaleViewModel>(sale);
        }        

        public async Task<SaleViewModel> CreateAsync(SaleInputModel entity)
        {
            var sale = _mapper.Map<SaleEntity>(entity);
            await _saleService.CreateAsync(sale);
            
            return _mapper.Map<SaleEntity,SaleViewModel>(sale);
        }

        public void UpdateStatus(Guid id, string status)
        {
            DomainExceptionValidation.When(!Enum.IsDefined(typeof(SaleStatus),status),"Invalid SaleStatus type.");
            
            var _status =  Enum.Parse<SaleStatus>(status);
            
            _saleService.UpdateStatus(id,_status);
        }

        public void Canceled(Guid id)
        {
            IsValid(id);
            _saleService.Canceled(id);
        }

        public void PaymentAccept(Guid id)
        {
            IsValid(id);
            _saleService.PaymentAccept(id);
        }

        public void SentCarrier(Guid id)
        {
            IsValid(id);
            _saleService.SentCarrier(id);
        }

        public void Delivered(Guid id)
        {
            IsValid(id);
            _saleService.Delivered(id);
        }

        private void IsValid(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty, "Invalid argument");
        }
    }
}