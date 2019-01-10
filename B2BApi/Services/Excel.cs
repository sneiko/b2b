using System.Data;
using System.IO;
using ExcelDataReader;

namespace B2BApi.Services
{
    public class Excel
    {
        public void Parse()
        {
            var path = "";
            FileStream Stream = new FileStream(path, FileMode.Open, FileAccess.Read); 
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateBinaryReader(Stream);
            excelDataReader[]
        }
    }
}