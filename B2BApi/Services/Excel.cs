using System;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace B2BApi.Services
{
    public class Excel
    {
        public object Parse(int id)
        {
            var path = "C:\\Users\\-__-\\Downloads\\Пилакос.xlsx";
            FileStream Stream = new FileStream(path, FileMode.Open, FileAccess.Read); 
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
                // Do Something
            }

            return dataSet.Tables.Count;
        }
    }
}