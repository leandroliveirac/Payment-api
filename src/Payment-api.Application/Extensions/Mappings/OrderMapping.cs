using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Extensions.Mappings
{
    public static class OrderMapping
    {
        public static OrderViewModel MapParaOrderViewModel(this OrderEntity src) 
            => src == null ? null : new()
        {
            Id = src.Id,
            Date = src.Date,
            Status = src.Status,
            Items = src.Items?.MapParaListOrderItemViewModel()
        };

        public static IEnumerable<OrderViewModel> MapParaListOrderViewModel(this IEnumerable<OrderEntity> src)
            => src?.Select(x => x.MapParaOrderViewModel()) ?? [];

        public static OrderEntity MapParaOrderEntity(this OrderInputModel src) 
            => src == null ? null : new();
    }
}