using AutoMapper;
using ProductDotnet.Models;
using ProductDotnet.Models.Dto;

namespace ProductDotnet.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
