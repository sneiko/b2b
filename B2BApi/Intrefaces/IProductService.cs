using System.Collections.Generic;
using System.Threading.Tasks;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResult<Product>> GetProductAsync(int productId);
        Task<ServiceResult<List<Product>>> GetProductListAsync();
        Task<ServiceResult> DeleteProductAsync(int productId);
        Task<ServiceResult> UpdateProductAsync(Product product);
        Task<ServiceResult> AddProductAsync(Product product);
    }
}