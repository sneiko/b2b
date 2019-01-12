using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using B2BApi.Models;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
using ExcelDataReader;

namespace B2BApi.Services
{
    public class Excel
    {
        public object Parse(int handlerId)
        {
            #region TestData
            
            Handler handler = new Handler{Id = 1,
                Name = "Sosiska", 
                Url = "https://www.dropbox.com/s/kv5bx2ncfz8bzhn/%D0%9D%D0%B0%D0%B4%D0%B5%D0%B6%D0%BD%D1%8B%D0%B5%20%D0%B8%D0%BD%D1%81%D1%82%D1%80%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D1%8B.xls?dl=1",
                SaveFileName = "C:\\Temp\\price.xls",
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

            #endregion

            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(new Uri(handler.Url), handler.SaveFileName);
            }
            using (FileStream stream = new FileStream(handler.SaveFileName, FileMode.Open, FileAccess.Read))
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
                    return DeleteColumns(handler, dataSet.Tables[0]); 


                return null;
            }
        }

        private static DataTable DeleteColumns(Handler handler, DataTable dataTable)
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

        private static DataSet ReplacePatterns(ICollection<Pattern> patterns, DataTable dataTable)
        {
            foreach (Pattern pattern in patterns)
            {

                foreach (DataRow dataTableRow in dataTable.Rows)
                {
                    dataTableRow[pattern.ColumnId] = dataTableRow[pattern.ColumnId].ToString()
                        .Replace(pattern.Old, pattern.New);
                }
            }

            return null;
        }
    }
}