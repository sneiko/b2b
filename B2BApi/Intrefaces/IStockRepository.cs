using System.Threading.Tasks;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Interfaces
{
    public interface IStockRepository : IRepository
    {
        Task<Stock> GetStockByProductAsync(int productId);
        Task<Stock> UpdateStock(Stock stock);
        Task<Stock> AddStock(Stock stock);
    }
}