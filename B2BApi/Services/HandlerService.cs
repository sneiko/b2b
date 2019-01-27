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

            var re = new Regex("(\\.(xlsx|xls|csv))");

            if (!re.IsMatch(handler.Url))
                return null;
            
            string fileExtension = re.Match(handler.Url).Groups[1].Value;
            
            using (var wc = new WebClient())
            {
                wc.DownloadFileAsync(new Uri(handler.Url), handler.SaveFileName + fileExtension);
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
                    var dataTable = DeleteColumns(handler, dataSet.Tables[0]);
                    //return ReplacePatterns(patterns, dataTable);
                    return new ServiceResult(ResultStatus.Success);
                }


                return null;
            }
        }

        private static  DataTable DeleteColumns(Handler handler, DataTable dataTable)
        {
            foreach (DataColumn c in dataTable.Columns)
            {
                int index = int.Parse(c.ColumnName.Replace("Column", ""));
                bool grab = handler.GrabColumnItems.Any(x => x.Value == index);
                if (grab == false)
                {
                    dataTable.Columns.Remove(c.ColumnName);
                    DeleteColumns(handler, dataTable);
                    break;
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