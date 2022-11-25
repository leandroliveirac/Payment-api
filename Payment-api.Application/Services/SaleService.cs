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
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
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

        public async Task CreateAsync(SaleInputModel entity)
        {
            var sale = _mapper.Map<SaleEntity>(entity);
            await _saleRepository.CreateAsync(sale);
        }

        public void Remove(SaleInputModel entity, Guid saleId)
        {
            var sale = _saleRepository.GetByIdAsync(saleId);
            if(sale == null)
                throw new NullReferenceException();

            _saleRepository.Remove(sale.Result);
        }

        public void UpdateStatus(Guid saleId, SaleStatus status)
        {
            var sale = _saleRepository.GetByIdAsync(saleId).Result;
            if(sale == null)
                throw new NullReferenceException();
            
            sale.UpdateStatus(status);
            _saleRepository.Update(sale);
        }
    }
}