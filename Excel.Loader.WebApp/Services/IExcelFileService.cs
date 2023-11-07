using Excel.Loader.WebApp.Models;
using OfficeOpenXml;

namespace Excel.Loader.WebApp.Services
{
    public interface IExcelFileService
    {
        Task SaveWorkbook(Stream xlsStream, string[] sheets);
    }    
}
