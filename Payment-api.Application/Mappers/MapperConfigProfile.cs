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
            CreateMap<CategoryEntity,CategoryInputModel>().ReverseMap();
            CreateMap<ProductEntity,ProductInputModel>().ReverseMap();
            CreateMap<SaleEntity,SaleInputModel>().ReverseMap();
            CreateMap<SellerEntity,SellerInputModel>().ReverseMap();
            CreateMap<OrderEntity,OrderInputModel>().ReverseMap();
            CreateMap<OrderItemEntity,OrderItemInputModel>().ReverseMap();
            CreateMap<PhoneEntity,PhoneInputModel>().ReverseMap();
            #endregion

            #region ViewModel
            CreateMap<CategoryEntity,CategoryViewModel>().ReverseMap();
            CreateMap<ProductEntity,ProductViewModel>().ReverseMap();
            CreateMap<SaleEntity,SaleViewModel>().ReverseMap();
            CreateMap<SellerEntity,SellerViewModel>().ReverseMap();
            CreateMap<OrderEntity,OrderViewModel>().ReverseMap();
            CreateMap<OrderItemEntity,OrderItemViewModel>().ReverseMap();
            CreateMap<PhoneEntity,PhoneViewModel>().ReverseMap();
            #endregion
        }
    }
}