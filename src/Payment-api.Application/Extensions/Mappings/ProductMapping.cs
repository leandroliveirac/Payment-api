using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Extensions.Mappings
{
    public static class ProductMapping
    {
        public static ProductViewModel MapParaProductViewModel(this ProductEntity src) => src == null ? null : new()
        {
            Id = src.Id,
            Description = src.Description,
            Price = src.Price,
            Active = src.Active,
            CategoryId = src.CategoryId,
            Category = src.Category?.MapParaCategoryViewModel()
        };

        public static ProductEntity MapParaProductEntity(this ProductInputModel src) 
            => src == null ? null : new(src.Description, src.Price, src.CategoryId, src.Active);

        public static IEnumerable<ProductViewModel> MapParaListProductViewModel(this IEnumerable<ProductEntity> src)
            => src?.Select(x => x.MapParaProductViewModel()) ?? [];
    }
}