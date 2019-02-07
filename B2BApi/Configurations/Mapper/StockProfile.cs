using AutoMapper;
using B2BApi.Models;
using B2BApi.Models.Helpers;

namespace B2BApi.Configurations.Mapper
{
    public class StockProfile : Profile
    {
        
        public StockProfile()
        {
            CreateMap<Stock, Stock>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Provider, opt => opt.Ignore())
                .ForMember(x => x.UpdateTime, opt => opt.Ignore());

            CreateMap<Price, Price>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.PriceType, opt => opt.Ignore());
        }
    }
}