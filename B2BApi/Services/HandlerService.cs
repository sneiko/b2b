using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using B2BApi.DbContext;
using B2BApi.Enums;
using B2BApi.Interfaces;
using B2BApi.Models;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
using B2BApi.ViewModels;
using ExcelDataReader;
using Microsoft.Extensions.Configuration;

namespace B2BApi.Services
{
    public class HandlerService : IHandlerService
    {
        private readonly IStockRepository StockRepository;
        private readonly IBrandRepository BrandRepository;
        private readonly IHandlerRepository HandlerRepository;
        private readonly IProductRepository ProductRepository;
        private readonly IMapper Mapper;
        private readonly B2BDbContext Context;
        private readonly IConfiguration Configuration;

        public HandlerService(IBrandRepository brandRepository, IHandlerRepository handlerRepository,
            IProductRepository productRepository, IStockRepository stockRepository, IMapper mapper,
            B2BDbContext context, IConfiguration configuration)
        {
            BrandRepository = brandRepository;
            StockRepository = stockRepository;
            HandlerRepository = handlerRepository;
            ProductRepository = productRepository;
            Mapper = mapper;
            Context = context;
        }

        /// <summary>
        /// Get handler object by ID
        /// </summary>
        /// <param name="handlerId"></param>
        /// <returns></returns>
        public async Task<ServiceResult<Handler>> GetHandlerAsync(int handlerId)
        {
            try
            {
                var handler = await HandlerRepository.GetHandlerAsync(handlerId);

                return new ServiceResult<Handler>(handler, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return new ServiceResult<Handler>(null, ResultStatus.Fail, "Сервис недоступен");
            }
        }

        /// <summary>
        /// Get all handler objects in List<Handler/> 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<List<Handler>>> GetHandlerListAsync()
        {
            try
            {
                List<Handler> handlers = await HandlerRepository.GetHandlerListAsync();

                return new ServiceResult<List<Handler>>(handlers, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return new ServiceResult<List<Handler>>(null, ResultStatus.Fail, "Сервис недоступен");
            }
        }


        /// <summary>
        /// Delete handler object from DB by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteHandlerAsync(int id)
        {
            try
            {
                await HandlerRepository.DeleteHandlerAsync(id);

                return new ServiceResult(ResultStatus.Success);
            }
            catch (Exception e)
            {
                return new ServiceResult(ResultStatus.Fail, "Сервис недоступен");
            }
        }

        /// <summary>
        /// Update handler in DB
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateHandlerAsync(Handler handler)
        {
            try
            {
                await HandlerRepository.UpdateHandler(handler);

                return new ServiceResult(ResultStatus.Success);
            }
            catch (Exception e)
            {
                return new ServiceResult(ResultStatus.Fail, "Сервис недоступен");
            }
        }

        /// <summary>
        /// Add handler object to DB
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public async Task<ServiceResult> AddHandlerAsync(Handler handler)
        {
            try
            {
                await HandlerRepository.AddHandler(handler);

                return new ServiceResult(ResultStatus.Success);
            }
            catch (Exception e)
            {
                return new ServiceResult(ResultStatus.Fail, "Сервис недоступен");
            }
        }


        // todo: доедалть имплементацию 


        public async Task<ServiceResult> Start(int handlerId)
        {
            try
            {
                var handler = await HandlerRepository.GetHandlerAsync(handlerId);
                var re = new Regex("(\\.(xlsx|xls|csv))");

                if (!re.IsMatch(handler.Url))
                    return new ServiceResult(ResultStatus.Fail, "Неверный Url прайса");

                string fileExtension = re.Match(handler.Url).Groups[1].Value;
                string fileName = String.Join("", handler.Provider.Name.Split(Path.GetInvalidFileNameChars()));
                string filePath = Configuration.GetConnectionString("excel") +
                                  fileName.Replace(" ", "") + fileExtension;

                using (var wc = new WebClient())
                {
                    wc.DownloadFile(new Uri(handler.Url), filePath);
                }


                using (var stream =
                    new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //TODO: need override CreateReader method
                    var excelDataReader = fileExtension == ".csv" ? 
                        ExcelReaderFactory.CreateCsvReader(stream) : ExcelReaderFactory.CreateReader(stream);


                    DataSet dataSet = excelDataReader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = false // Use first row is ColumnName here :D
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        var grabColumns = handler.GrabColumnItems;

                        var products = new List<Product>();
                        var stocks = new List<Stock>();
                        var brands = new List<Brand>();
                        for (var index = handler.StartRowData; index < dataTable.Rows.Count; index++)
                        {
                            var row = dataTable.Rows[index];

                            var (isSuccess, product, stock, brand) = await FastParseRow(row, handler, grabColumns);
                            
                            if (isSuccess == false)
                                continue;
                            
                            products.Add(product);
                            stocks.Add(stock);

                            if (string.IsNullOrEmpty(brand.Name) == false &&
                                brands.Any(b =>
                                    b.Name.Equals(brand.Name, StringComparison.InvariantCultureIgnoreCase)) == false)
                            {
                                brands.Add(brand);
                            }
                        }


                        var dbProducts =
                            await ProductRepository.GetProductsForParseAsync(products, handler.Provider.Id);
                        var newProducts = products.Where(p => dbProducts.All(d => d.PartNumber != p.PartNumber))
                            .ToList();

                        if (handler.AddNewProduct && newProducts.Any())
                        {
                            var dbBrands = await BrandRepository.GetBrandsByNameAsync(brands);
                            var newBrands = brands.Where(b =>
                                    dbBrands.Any(d =>
                                        d.Name.Equals(b.Name, StringComparison.InvariantCultureIgnoreCase)) == false)
                                .ToList();
                            if (newBrands.Any())
                            {
                                await Context.Brands.AddRangeAsync(newBrands);
                                dbBrands.AddRange(newBrands);
                            }

                            //TODO: хранить в продукте ссылку на объект Brand
                            newProducts.ForEach(p => p.Brand = dbBrands.FirstOrDefault(b =>
                                p.Brand.Name.Equals(b.Name, StringComparison.InvariantCultureIgnoreCase)));

                            await Context.Products.AddRangeAsync(newProducts);
                        }

                        var dbStocks = await StockRepository.GetStocksForParserAsync(dbProducts, handler.Provider.Id);
                        var newStocks = stocks
                            .Where(s => dbStocks.All(d => d.Product.PartNumber != s.Product.PartNumber)).ToList();
                        
                        if (newStocks.Any())
                            dbProducts.ForEach(p =>
                            {
                                var stock = newStocks.FirstOrDefault(s => s.Product.PartNumber == p.PartNumber);
                                if (stock != null)
                                    stock.Product = p;
                            });
                        //newStocks.ForEach(s => s.Product = dbProducts.FirstOrDefault(p => p.PartNumber == s.Product.PartNumber));
                        //var updateStocks = stocks
                        //    .Where(s => newStocks.All(n => n.Product.PartNumber != s.Product.PartNumber)).ToList();

                        // Update
                        dbStocks.ForEach(d => 
                            Mapper.Map(stocks.FirstOrDefault(u => u.Product.PartNumber == d.Product.PartNumber), d));
                        //updateStocks.ForEach(s =>
                        //    Mapper.Map(s, dbStocks.FirstOrDefault(d => d.Product.PartNumber == s.Product.PartNumber)));

                        if (newStocks.Any())
                            await Context.StockProducts.AddRangeAsync(newStocks);

                        await Context.SaveChangesAsync();
                        return new ServiceResult(ResultStatus.Success);
                    }
                }

                return new ServiceResult(ResultStatus.Fail, "");
            }
            catch (Exception e)
            {
                return new ServiceResult(ResultStatus.Fail, "Не удалось обработать прайс");
            }
        }

        private async Task<(bool isSuccess, Product product, Stock stock, Brand brand)> FastParseRow(DataRow row, Handler handler,
            ICollection<GrabColumnItem> grabColumns)
        {
            foreach (var pattern in handler.Patterns)
            {
                row[pattern.ColumnId] =
                    row[pattern.ColumnId].ToString().Replace(pattern.Old, pattern.New);
            }

            var partIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.PartNumber).Value;
            var partnumber = row[partIndex].ToString();
            
            if (partnumber.Length <= 1)
                return (false, null, null, null);

            var modelIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.Model).Value;
            var model = row[modelIndex].ToString();

            var brandIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.Brand).Value;
            var brandString = row[brandIndex].ToString();
            
            if (brandString.Length <= 1)
                return (false, null, null, null);

            var brand = new Brand
            {
                Name = brandString
            };

            var newProduct = new Product
            {
                Model = model,
                PartNumber = partnumber,
                Brand = brand,
                UpdateTime = DateTime.Now
            };


            var countIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.Count).Value;
            var rawCount = Regex.Replace(row[countIndex].ToString(), "\\D", "");
            var count = int.Parse(rawCount);

            var priceIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.Price).Value;
            var price = double.Parse(row[priceIndex].ToString());

            var newStock = new Stock
            {
                PriceType = PriceType.Cash, // temp
                Price = price,
                Count = count,
                Product = newProduct,
                Provider = handler.Provider,
                UpdateTime = DateTime.Now
            };

            return
                (isSuccess: true, product: newProduct, stock: newStock, brand);
        }
    }
}