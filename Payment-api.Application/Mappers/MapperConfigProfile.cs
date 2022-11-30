using AutoMapper;
using Payment_api.Application.InputModels;
using Payment_api.Application.ViewModels;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Mappers
{
    public class MapperConfigProfile : Profile
    {        
        public MapperConfigProfile()
        {
            #region  InputModel
            CreateMap<CategoryInputModel, CategoryEntity>();
            CreateMap<ProductInputModel, ProductEntity>();
            CreateMap<SaleInputModel, SaleEntity>();
            CreateMap<SellerInputModel, SellerEntity>();
            CreateMap<OrderInputModel, OrderEntity>();
            CreateMap<OrderItemInputModel, OrderItemEntity>();
            CreateMap<PhoneInputModel, PhoneEntity>();
            #endregion

            #region ViewModel
            CreateMap<CategoryEntity,CategoryViewModel>();
            CreateMap<ProductEntity,ProductViewModel>();
            CreateMap<SaleEntity,SaleViewModel>();
            CreateMap<SellerEntity,SellerViewModel>();
            CreateMap<OrderEntity,OrderViewModel>();
            CreateMap<OrderItemEntity,OrderItemViewModel>();
            CreateMap<PhoneEntity,PhoneViewModel>();
            #endregion
        }
    }
}