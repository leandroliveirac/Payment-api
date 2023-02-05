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
    public sealed class SellerAppService : ISellerAppService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly ISellerService _sellerService;
        private readonly IMapper _mapper;

        public SellerAppService(ISellerRepository sellerRepository, IMapper mapper, ISellerService sellerService)
        {
            _sellerRepository = sellerRepository;
            _mapper = mapper;
            _sellerService = sellerService;
        }

        public async Task<IEnumerable<SellerViewModel>> GetAllAsync()
        {
            var sellers = await _sellerRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SellerViewModel>>(sellers);
        }

        public async Task<SellerViewModel> GetByIdAsync(Guid id)
        {
            IsValid(id);

            var seller = await _sellerRepository.GetByIdAsync(id);

            if(seller == null) return null;

            return _mapper.Map<SellerViewModel>(seller);
        }

        public async Task<SellerViewModel> CreateAsync(SellerInputModel entity)
        {
            HasPhones(entity.Phones);

            var seller = _mapper.Map<SellerInputModel, SellerEntity>(entity, opt =>
            {
                opt.BeforeMap((src, dest) => src.Phones.ToList().ForEach(x => { x.Id = Guid.Empty; }));
            });

            await _sellerService.CreateAsync(seller);

            return _mapper.Map<SellerViewModel>(seller);
        }

        public void Remove(Guid id)
        {
            IsValid(id);
            
            SellerEntity seller = GetById(id);

            _sellerService.Remove(seller);
        }

        public SellerViewModel Update(SellerInputModel entity, Guid id)
        {
            IsValid(id);

            HasPhones(entity.Phones);

            SellerEntity seller = GetById(id);

            _sellerService.Update(seller, _mapper.Map<SellerEntity>(entity));

            return _mapper.Map<SellerViewModel>(seller);
        }

        private SellerEntity GetById(Guid id)
        {
            var seller = _sellerRepository.GetByIdAsync(id).Result;

            DomainExceptionValidation.When(seller == null, "Seller not found");
            return seller;
        }

        private void HasPhones(IEnumerable<PhoneInputModel> phones)
        {
            DomainExceptionValidation.When(phones == null || phones.Count() <= 0, "Phone is required. Enter at least one phone number");
        }

        private void IsValid(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty, "Invalid argument");
        }
    }
}
