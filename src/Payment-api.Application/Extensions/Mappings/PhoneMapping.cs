using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Extensions.Mappings
{
    public static class PhoneMapping
    {
        public static PhoneViewModel MapParaPhoneViewModel(this PhoneEntity src) => src == null ? null : new()
        {
            Id = src.Id,
            Ddd = src.Ddd,
            Number = src.Number,
            Type = src.Type
        };

        public static PhoneEntity MapParaPhoneEntity(this PhoneInputModel src) 
            => src == null ? null : new(src.Ddd,src.Number,src.Type,src.SellerId);

        public static IEnumerable<PhoneViewModel> MapParaListPhoneViewModel(this IEnumerable<PhoneEntity> src)
            => src?.Select(x => x.MapParaPhoneViewModel()) ?? [];

        public static List<PhoneEntity> MapParaListPhoneEntity(this IEnumerable<PhoneInputModel> src)
            => src?.Select(x => x.MapParaPhoneEntity()).ToList() ?? [];
    }
}