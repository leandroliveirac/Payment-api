using Payment_api.Application.Extensions.Mappings;
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

        public SellerAppService(ISellerRepository sellerRepository, ISellerService sellerService)
        {
            _sellerRepository = sellerRepository;
            _sellerService = sellerService;
        }

        public async Task<IEnumerable<SellerViewModel>> GetAllAsync()
        {
            var sellers = await _sellerRepository.GetAllAsync();

            return sellers.MapParaListSellerViewModel();
        }

        public async Task<SellerViewModel> GetByIdAsync(Guid id)
        {
            IsValid(id);

            var seller = await _sellerRepository.GetByIdAsync(id);

            if(seller == null) return null;

            return seller.MapParaSellerViewModel();
        }

        public async Task<SellerViewModel> CreateAsync(SellerInputModel entity)
        {
            HasPhones(entity.Phones);

            var sellerEntity = entity.MapParaSellerEntity();

            var sellerResult = await _sellerService.CreateAsync(sellerEntity);

            return sellerResult.MapParaSellerViewModel();
        }

        public async Task RemoveAsync(Guid id)
        {
            IsValid(id);
            
            var seller = await _sellerRepository.GetByIdAsync(id);

            DomainExceptionValidation.When(seller == null, "Seller not found");

            _sellerService.Remove(seller);
        }

        public async Task<SellerViewModel> UpdateAsync(SellerInputModel entity, Guid id)
        {
            IsValid(id);

            HasPhones(entity.Phones);

            var seller = await _sellerRepository.GetByIdAsync(id);

            DomainExceptionValidation.When(seller == null, "Seller not found");

            _sellerService.Update(seller, entity.MapParaSellerEntity());

            return seller.MapParaSellerViewModel();
        }

        private static void HasPhones(IEnumerable<PhoneInputModel> phones)
        {
            DomainExceptionValidation.When(phones == null || phones.Count() <= 0, "Phone is required. Enter at least one phone number");
        }

        private static void IsValid(Guid id)
        {
            DomainExceptionValidation.When(id == Guid.Empty, "Invalid argument");
        }
    }
}
