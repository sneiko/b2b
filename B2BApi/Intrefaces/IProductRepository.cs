using System.Collections.Generic;
using System.Threading.Tasks;
using B2BApi.Models;

namespace B2BApi.Intrefaces
{
    public interface IProductRepository : IRepository
    {
        Task<Product> GetProductAsync(int productId);
        Task<Product> GetProductAsync(string partNumber);
        Task DeleteProductAsync(int productId);
        Task<List<Product>> GetProductListAsync();
        Task UpdateProduct(Product product);
        Task AddProduct(Product product);
    }
}