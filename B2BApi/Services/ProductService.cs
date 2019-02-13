using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using B2BApi.Enums;
using B2BApi.Interfaces;
using B2BApi.Models;
using B2BApi.Repositories;
using B2BApi.ViewModels;

namespace B2BApi.Services {
    public class ProductService : IProductService {
        private readonly IProductRepository ProductRepository;
        private readonly IMapper            Mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper) {
            ProductRepository = productRepository;
            Mapper            = mapper;
        }

        public async Task<ServiceResult<Product>> GetProductAsync(int productId) {
            try {
                var product = await ProductRepository.GetProductAsync(productId);

                return new ServiceResult<Product>(product, ResultStatus.Success);
            }
            catch (Exception e) {
                return new ServiceResult<Product>(null, ResultStatus.Fail, "Сервис недоступен");
            }
        }

        public async Task<ServiceResult<List<Product>>> GetProductListAsync() {
            try {
                var products = await ProductRepository.GetProductListAsync();

                return new ServiceResult<List<Product>>(products, ResultStatus.Success);
            }
            catch (Exception e) {
                return new ServiceResult<List<Product>>(null, ResultStatus.Fail, "Сервис недоступен");
            }
        }

        public async Task<ServiceResult> DeleteProductAsync(int productId) {
            try {
                await ProductRepository.DeleteProductAsync(productId);
                return new ServiceResult(ResultStatus.Success);
            }
            catch (Exception e) {
                return new ServiceResult<List<Product>>(null, ResultStatus.Fail, "Сервис недоступен");
            }
        }

        public async Task<ServiceResult> UpdateProductAsync(Product product) {
            try {
                await ProductRepository.UpdateProduct(product);
                return new ServiceResult(ResultStatus.Success);
            }
            catch (Exception e) {
                return new ServiceResult(ResultStatus.Fail, "Сервис недоступен");
            }
        }

        public async Task<ServiceResult> AddProductAsync(Product product) {
            try {
                await ProductRepository.AddProduct(product);
                return new ServiceResult(ResultStatus.Success);
            }
            catch (Exception e) {
                return new ServiceResult(ResultStatus.Fail, "Сервис недоступен");
            }
        }
    }
}