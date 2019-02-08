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

namespace B2BApi.Services
{
    public class HandlerService : IHandlerService
    {
        private readonly IStockRepository StockRepository;
        private readonly IBrandRepository BrandRepository;
        private readonly IHandlerRepository HandlerRepository;
        private readonly IProductRepository ProductRepository;
        private readonly IMapper Mapper;

        public HandlerService(IBrandRepository brandRepository, IHandlerRepository handlerRepository,
            IProductRepository productRepository, IStockRepository stockRepository, IMapper mapper)
        {
            BrandRepository = brandRepository;
            StockRepository = stockRepository;
            HandlerRepository = handlerRepository;
            ProductRepository = productRepository;
            Mapper = mapper;
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
                string filePath = fileName.Replace(" ", "") + fileExtension;

                using (var wc = new WebClient())
                {
                    wc.DownloadFile(new Uri(handler.Url), filePath);
                }


                using (var stream =
                    new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelDataReader = ExcelReaderFactory.CreateReader(stream);
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
                        for (var index = handler.StartRowData; index < dataTable.Rows.Count; index++)
                        {
                            DataRow row = dataTable.Rows[index];
                            foreach (var pattern in handler.Patterns)
                            {
                                row[pattern.ColumnId] =
                                    row[pattern.ColumnId].ToString().Replace(pattern.Old, pattern.New);
                            }

                            var partIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.PartNumber).Value;
                            var partnumber = row[partIndex].ToString();

                            var product = await ProductRepository.GetProductAsync(partnumber);

                            if (product == null)
                            {
                                var modelIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.Model).Value;
                                var model = row[modelIndex].ToString();

                                var brandIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.Brand).Value;
                                var brandString = row[brandIndex].ToString();

                                var brand = await BrandRepository.GetBrandAsync(brandName: brandString) ?? new Brand
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

                                product = await ProductRepository.AddProduct(newProduct);
                            }

                            if (product != null)
                            {
                                var countIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.Count).Value;
                                var count = int.Parse(row[countIndex].ToString());

                                var priceIndex = grabColumns.First(x => x.GrabColumn == GrabColumn.Price).Value;
                                var price = double.Parse(row[priceIndex].ToString());

                                var newStock = new Stock
                                {
                                    PriceType = PriceType.Cash, // temp
                                    Price = price,
                                    Count = count,
                                    Product = product,
                                    Provider = handler.Provider,
                                    UpdateTime = DateTime.Now
                                };
                                var oldStock = await StockRepository.GetStockAsync(product.Id, handler.Provider.Id);

                                if (oldStock != null)
                                {
                                    await StockRepository.UpdateStock(newStock, oldStock);
                                }
                                else if (handler.AddNewProduct)
                                {
                                    await StockRepository.AddStock(newStock);
                                }
                            }
                        }

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
    }
}