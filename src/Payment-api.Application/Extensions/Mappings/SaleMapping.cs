using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Extensions.Mappings
{
    public static class SaleMapping
    {
        public static SaleViewModel MapParaSaleViewModel(this SaleEntity src) => src == null ? null : new()
        {
            Id = src.Id,
            Moment = src.Moment,
            Status = src.Status,
            Seller = src.Seller?.MapParaSellerViewModel(),
            Order = src.Order?.MapParaOrderViewModel()
        };

        public static SaleEntity MapParaSaleEntity(this SaleInputModel src) 
            => src == null ? null : new(src.SellerId,src.OrderId);

        public static IEnumerable<SaleViewModel> MapParaListSaleViewModel(this IEnumerable<SaleEntity> src)
            => src?.Select(x => x.MapParaSaleViewModel()) ?? [];
    }
}