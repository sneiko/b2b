using System;
using System.Data;
using System.IO;
using System.Net;
using ExcelDataReader;

namespace B2BApi.Services
{
    public class Excel
    {
        public object Parse(int id, string url)
        {
            string fileName = "C:\\Temp\\price.xls";
            url =
                "https://www.dropbox.com/s/kv5bx2ncfz8bzhn/%D0%9D%D0%B0%D0%B4%D0%B5%D0%B6%D0%BD%D1%8B%D0%B5%20%D0%B8%D0%BD%D1%81%D1%82%D1%80%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D1%8B.xls?dl=1";
            byte[] xls;
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(new System.Uri(url), fileName);
            }

            using (FileStream Stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateReader(Stream);
                DataSet dataSet = excelDataReader.AsDataSet(new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = false // Use first row is ColumnName here :D
                    }
                });
                if (dataSet.Tables.Count > 0)
                {
                    var dtData = dataSet.Tables[0];
                    return dtData.Rows[id][4].ToString();
                }

                return dataSet.Tables.Count;
            }
        }
    }
}