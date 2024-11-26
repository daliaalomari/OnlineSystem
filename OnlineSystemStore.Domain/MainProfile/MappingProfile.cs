using AutoMapper;
using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Domain.Tables;


namespace OnlineSystemStore.Domain.MainProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
