using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Extensions.Mappings
{
    public static class CategoryMapping
    {
        public static CategoryViewModel MapParaCategoryViewModel(this CategoryEntity src) 
            => src == null ? null : new()
        {
            Id = src.Id,
            Description = src.Description
        };

        public static CategoryEntity MapParaCategoryEntity(this CategoryInputModel src) 
            => src == null ? null : new(src.Description);

        public static IEnumerable<CategoryViewModel> MapParaListCategoryViewModel(this IEnumerable<CategoryEntity> src)
            => src?.Select(x => x.MapParaCategoryViewModel()) ?? [];
    }
}