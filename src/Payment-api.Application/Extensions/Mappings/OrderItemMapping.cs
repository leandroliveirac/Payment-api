using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Extensions.Mappings
{
    public static class OrderItemMapping
    {
        public static OrderItemViewModel MapParaOrderItemViewModel(this OrderItemEntity src) 
            => src == null ? null : new()
        {
            Id = src.Id,
            ProductId = src.ProductId,
            ProductDescription = src.Product?.Description,
            ProductUnitPrice = src.ProductUnitPrice,
            Quantity = src.Quantity
        };

        public static IEnumerable<OrderItemViewModel> MapParaListOrderItemViewModel(this IEnumerable<OrderItemEntity> src)
            => src?.Select(x => x.MapParaOrderItemViewModel()) ?? [];
            
        // Note: Se houver OrderItemInputModel, adicione o mapeamento para Entity aqui seguindo o padrão.
    }
}