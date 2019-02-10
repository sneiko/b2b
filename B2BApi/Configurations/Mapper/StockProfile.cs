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
                .ForMember(x => x.PriceType, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Provider, opt => opt.Ignore())
                .ForMember(x => x.ProductId, opt => opt.Ignore())    
                .ForMember(x => x.ProviderId, opt => opt.Ignore());
        }
    }
}