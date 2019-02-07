using System.Threading.Tasks;
using AutoMapper;
using B2BApi.DbContext;
using B2BApi.Interfaces;
using B2BApi.Models;
using B2BApi.Models.Helpers;
using B2BApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Repositories
{
    public class StockRepository : BaseRepository, IStockRepository
    {
        public StockRepository(B2BDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Stock> GetStockByProductAsync(int productId)
            =>
            await Context.StockProducts.FirstOrDefaultAsync(x => x.Product.Id == productId);

        public async Task<Stock> UpdateStock(Stock stock)
        {
            var entry = await Context.StockProducts
                .Include(x => x.Price)
                .FirstOrDefaultAsync(x => x.Provider.Id == stock.Provider.Id);
            Mapper.Map(stock, entry);
            await Context.SaveChangesAsync();
            return entry;
        }

        public async Task<Stock> AddStock(Stock stock)
        {
            var entry = await Context.StockProducts.AddAsync(stock);
            await Context.SaveChangesAsync();
            return entry.Entity;
        }
    }
}