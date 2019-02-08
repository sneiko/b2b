using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using B2BApi.DbContext;
using B2BApi.Interfaces;
using B2BApi.Models;
using B2BApi.Models.Helpers;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Repositories
{
    public class ProductRepository: BaseRepository, IProductRepository
    {
        public ProductRepository(B2BDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        /// <summary>
        /// Get product from DB
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> GetProductAsync(int productId)
            => await Context.Products
                .Include(x => x.Brand)
                .Include(x => x.Stocks)
                .FirstOrDefaultAsync(x => x.Id == productId);

        /// <summary>
        /// Get product by partNumber
        /// </summary>
        /// <param name="partNumber"></param>
        /// <returns></returns>
        public async Task<Product> GetProductAsync(string partNumber)
            => await Context.Products
                .FirstOrDefaultAsync(x => x.PartNumber == partNumber);
        
        /// <summary>
        /// Get product list from DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetProductListAsync()
            => await Context.Products.ToListAsync();
        
        /// <summary>
        /// Delete product from DB
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task DeleteProductAsync(int productId)
            => Context.Products.Remove(
                await Context.Products.FirstOrDefaultAsync(x => x.Id == productId)
            );

        /// <summary>
        /// Update product in DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateProduct(Product product)
        {
            Context.Products.Update(product);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Add product to DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> AddProduct(Product product)
        {
            var entry = Context.Products.Add(product);
            await Context.SaveChangesAsync();
            return entry.Entity;
        }
    }
}