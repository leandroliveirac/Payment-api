using Payment_api.Application.Extensions.Mappings;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
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

        public SaleAppService(ISaleRepository saleRepository, IOrderRepository orderRepository, ISaleService saleService)
        {
            _saleRepository = saleRepository;
            _orderRepository = orderRepository;
            _saleService = saleService;
        }

        public async Task<IEnumerable<SaleViewModel>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            
            return sales.MapParaListSaleViewModel();
        }

        public async Task<SaleViewModel> GetByIdAsync(Guid id)
        {
            IsValid(id);

            var sale = await _saleRepository.GetByIdAsync(id);
            
            DomainExceptionValidation.When(sale == null, "Not found");

            return sale.MapParaSaleViewModel();
        }        

        public async Task<SaleViewModel> CreateAsync(SaleInputModel entity)
        {
            var sale = entity.MapParaSaleEntity();
            await _saleService.CreateAsync(sale);
            
            return sale.MapParaSaleViewModel();
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

        private static void IsValid(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty, "Invalid argument");
        }
    }
}