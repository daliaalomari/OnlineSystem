using AutoMapper;
using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Domain.Tables;


namespace OnlineSystemStore.Domain.MainProfile
{
    public class Main : Profile
    {
        public Main()
        {
            CreateMap<ProductDto, Category>();
            CreateMap<Category, ProductDto>();
        }
    }
}
