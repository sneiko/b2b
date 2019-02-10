using System.Collections.Generic;
using System.Threading.Tasks;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Interfaces
{
    public interface IStockRepository : IRepository
    {
        Task<Stock> GetStockAsync(int productId, int providerId);
        Task<List<Stock>> GetStocksForParserAsync(List<Product> products, int providerId);
        Task<Stock> UpdateStock(Stock newStock, Stock oldStock = null);
        Task<Stock> AddStock(Stock stock);
    }
}