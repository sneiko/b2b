using System.Collections.Generic;
using System.Threading.Tasks;
using B2BApi.Intrefaces;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Services
{
    public class ProductService: IProductService
    {
        public Task<ServiceResult<Product>> GetProductAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult<List<Product>>> GetProductListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> DeleteProductAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> UpdateProductAsync(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> AddProductAsync(Product product)
        {
            throw new System.NotImplementedException();
        }
    }
}