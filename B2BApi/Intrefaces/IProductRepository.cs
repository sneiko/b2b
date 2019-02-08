using System.Collections.Generic;
using System.Threading.Tasks;
using B2BApi.Models;

namespace B2BApi.Interfaces
{
    public interface IProductRepository : IRepository
    {
        Task<Product> GetProductAsync(int productId);
        Task<Product> GetProductAsync(string partNumber);
        Task DeleteProductAsync(int productId);
        Task<List<Product>> GetProductListAsync();
        Task UpdateProduct(Product product);
        Task<Product> AddProduct(Product product);
    }
}