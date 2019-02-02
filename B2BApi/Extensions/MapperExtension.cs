using System;
using AutoMapper;
using B2BApi.Configurations.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
            => services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            }, AppDomain.CurrentDomain.GetAssemblies());
    }
}