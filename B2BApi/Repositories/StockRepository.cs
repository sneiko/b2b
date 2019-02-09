using System.Collections.Generic;
using System.Linq;
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

        public async Task<Stock> GetStockAsync(int productId, int providerId)
            =>
                await Context.StockProducts
                    .FirstOrDefaultAsync(x => x.Product.Id == productId &&
                                              x.Provider.Id == providerId);

        public async Task<List<Stock>> GetStocksForParserAsync(List<Product> products, int providerId)
        {
            return await Context.StockProducts
                .Include(s => s.Product)
                .Where(s => s.ProviderId == providerId && products.Any(p => p.Id == s.ProductId))
                .ToListAsync();
        }

        public async Task<Stock> UpdateStock(Stock newStock, Stock oldStock = null)
        {
            if (oldStock == null)
                oldStock = await GetStockAsync(newStock.Product.Id, newStock.Provider.Id);

            Mapper.Map(newStock, oldStock);
            await Context.SaveChangesAsync();
            return oldStock;
        }

        public async Task<Stock> AddStock(Stock stock)
        {
            var entry = await Context.StockProducts.AddAsync(stock);
            await Context.SaveChangesAsync();
            return entry.Entity;
        }
    }
}