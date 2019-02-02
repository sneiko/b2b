using AutoMapper;
using B2BApi.Models;

namespace B2BApi.Configurations.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, Product>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}