using AutoMapper;
using Payment_api.Application.DTOs;
using Payment_api.Domain.Entities;

namespace Payment_api.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
        }
    }
}