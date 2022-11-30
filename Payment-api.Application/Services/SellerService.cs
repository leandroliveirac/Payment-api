using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.Interfaces.Services;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;
using Payment_api.Domain.Interfaces.Repositories;
using Payment_api.Domain.Validation;

namespace Payment_api.Application.Services
{
    public sealed class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;

        public SellerService(ISellerRepository sellerRepository, IMapper mapper, IPhoneRepository phoneRepository)
        {
            _sellerRepository = sellerRepository;
            _mapper = mapper;
            _phoneRepository = phoneRepository;
        }        

        public async Task<IEnumerable<SellerViewModel>> GetAllAsync()
        {
            var sellers = await _sellerRepository.GetAllAsync();

            if(sellers == null || sellers.Count() <= 0) return Enumerable.Empty<SellerViewModel>();

            return _mapper.Map<IEnumerable<SellerViewModel>>(sellers);
        }

        public async Task<SellerViewModel> GetByIdAsync(Guid id)
        {
            var seller = await _sellerRepository.GetByIdAsync(id);

            if(seller == null) return null;

            return _mapper.Map<SellerViewModel>(seller);
        }

        public async Task<SellerViewModel> CreateAsync(SellerInputModel entity)
        {
            try
            {
                DomainExceptionValidation.When(entity.Phones == null || entity.Phones.Count() <= 0, "Phone is required. Enter at least one phone number");

                var seller = _mapper.Map<SellerEntity>(entity);

                await _sellerRepository.CreateAsync(seller);

                return _mapper.Map<SellerViewModel>(seller);
            }
            catch
            {
                throw;
            }
        }

        public void Remove(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Invalid argument");

            var seller = _sellerRepository.GetByIdAsync(id).Result;

            if(seller == null) throw new ArgumentException("Invalid argument");

            _sellerRepository.Remove(seller);
        }

        public SellerViewModel Update(SellerInputModel entity, Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Invalid argument");

            var seller = _sellerRepository.GetByIdAsync(id).Result;

            if (seller == null) throw new ArgumentException("Invalid argument");

            if(seller.Phones != null) 
                _phoneRepository.RemoveRange(seller.Phones);

            var phones = _mapper.Map<IEnumerable<PhoneEntity>>(entity.Phones);

            seller.Phones = phones;

            seller.Update(entity.Name, entity.Email);

            _sellerRepository.Update(seller);

            return _mapper.Map<SellerViewModel>(seller);
        }
    }
}
