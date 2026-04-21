using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Extensions.Mappings
{
    public static class SellerMapping
    {
        public static SellerViewModel MapParaSellerViewModel(this SellerEntity src) => src == null ? null : new()
        {
            Id = src.Id,
            Name = src.Name,
            Email = src.Email,
            Cpf = src.Cpf,
            Phones = src.Phones?.MapParaListPhoneViewModel()
        };

        public static SellerEntity MapParaSellerEntity(this SellerInputModel src) 
            => src == null ? null : new(src.Name, src.Email, src.Cpf)
            {
                Phones = src.Phones?.MapParaListPhoneEntity() ?? []
            };

        public static IEnumerable<SellerViewModel> MapParaListSellerViewModel(this IEnumerable<SellerEntity> src)
            => src?.Select(x => x.MapParaSellerViewModel()) ?? [];
    }
}