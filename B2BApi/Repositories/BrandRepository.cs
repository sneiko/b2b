using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using B2BApi.DbContext;
using B2BApi.Interfaces;
using B2BApi.Models;
using B2BApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Repositories
{
    public class BrandRepository : BaseRepository, IBrandRepository
    {
        public BrandRepository(B2BDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Brand> GetBrandAsync(int brandId)
            =>
                await Context.Brands.FirstOrDefaultAsync(x => x.Id == brandId);

        public async Task<Brand> GetBrandAsync(string brandName)
            =>
                await Context.Brands.FirstOrDefaultAsync(x => x.Name.Equals(brandName,
                    StringComparison.InvariantCultureIgnoreCase));

        public async Task<List<Brand>> GetBrandsByNameAsync(List<Brand> brands)
            =>
                await Context.Brands
                    .Where(b => brands.Any(x => x.Name.Equals(b.Name,
                        StringComparison.InvariantCultureIgnoreCase)))
                    .ToListAsync();
    }
}