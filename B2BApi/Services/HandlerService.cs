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
using B2BApi.DbContext;
using B2BApi.Enums;
using B2BApi.Intrefaces;
using B2BApi.Models;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
using B2BApi.ViewModels;
using ExcelDataReader;

namespace B2BApi.Services
{
    public class HandlerService: IHandlerService
    {
        protected readonly IHandlerRepository HandlerRepository;

        public HandlerService(IHandlerRepository handlerRepository)
        {
            HandlerRepository = handlerRepository;
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
        /// Get all handler objects in List<Handler> 
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
            #region TestData

            var handler = new Handler{Id = 1,
                Name = "Sosiska", 
                Url = "https://www.dropbox.com/s/kv5bx2ncfz8bzhn/%D0%9D%D0%B0%D0%B4%D0%B5%D0%B6%D0%BD%D1%8B%D0%B5%20%D0%B8%D0%BD%D1%81%D1%82%D1%80%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D1%8B.xls?dl=1",
                SaveFileName = "C:\\Temp\\pochinki",
                StartRowData = 1,
                LastUpdate = DateTime.Now,
                Provider = new Provider(),
                GrabColumnItems = new List<GrabColumnItem>
                {
                    new GrabColumnItem{Id = 1, GrabColumn = GrabColumn.Model, Value = 4},
                    new GrabColumnItem{Id = 2, GrabColumn = GrabColumn.Brand, Value = 7},
                    new GrabColumnItem{Id = 3, GrabColumn = GrabColumn.PartNumber, Value = 3},
                    new GrabColumnItem{Id = 4, GrabColumn = GrabColumn.Price, Value = 11},
                    new GrabColumnItem{Id = 5, GrabColumn = GrabColumn.Count, Value = 8}
                }
            };
            var patterns = new List<Pattern>
            {
                new Pattern{ Id = 1, ColumnId = 3, Old = "н", New = "0"},
                new Pattern{ Id = 2, ColumnId = 3, Old = "м", New = "5"},
                new Pattern{ Id = 3, ColumnId = 3, Old = "c", New = "10"},
                new Pattern{ Id = 4, ColumnId = 3, Old = "б", New = "50"}
            };

            #endregion
            
            try
            {
                var re = new Regex("(\\.(xlsx|xls|csv))");

                if (!re.IsMatch(handler.Url))
                    return null;
            
                string fileExtension = re.Match(handler.Url).Groups[1].Value;
            
                using (var wc = new WebClient())
                {
                    wc.DownloadFile(new Uri(handler.Url), handler.SaveFileName + fileExtension);
                }


                using (var stream = new FileStream(handler.SaveFileName + fileExtension, FileMode.Open, FileAccess.Read))
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
                        // write to db
//                        using (var dbContext = new B2BDbContext())
//                        {
                            var dataTable = DeleteColumns(handler, dataSet.Tables[0]);
                            
//                            todo: дописать запись в бд
//                            List<Product> handlers = dbContext.Products.ToList();
//
//                            foreach (DataRow row in dataTable.Rows)
//                            {
//                                handlers.Find(p => p.PartNumber == row[""]);
//                            }


//                        }
                        //return ReplacePatterns(patterns, dataTable);
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

        private static  DataTable DeleteColumns(Handler handler, DataTable dataTable)
        {   
            foreach (DataColumn c in dataTable.Columns)
            {
                int index;
                if (!Int32.TryParse(c.ColumnName.Replace("Column", ""), out index))
                    continue;
                
                bool grab = handler.GrabColumnItems.Any(x => x.Value == index);
                if (grab == false)
                {
                    dataTable.Columns.Remove(c.ColumnName);
                    DeleteColumns(handler, dataTable);
                    break;
                } else {
                    var newColumnName = handler.GrabColumnItems.First(x => x.Value == index);
                    dataTable.Columns[c.ColumnName].ColumnName = newColumnName.GrabColumn.ToString();
                }
            }

            return dataTable;
        }

        private static DataTable ReplacePatterns(ICollection<Pattern> patterns, DataTable dataTable)
        {   
            foreach (DataRow dataTableRow in dataTable.Rows)
                foreach (var pattern in patterns)
                    dataTableRow[pattern.ColumnId] =
                        dataTableRow[pattern.ColumnId].ToString().Replace(pattern.Old, pattern.New);

            return dataTable;
        }
    }
}