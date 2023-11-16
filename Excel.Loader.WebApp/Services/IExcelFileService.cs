using Excel.Loader.WebApp.Models;
using OfficeOpenXml;

namespace Excel.Loader.WebApp.Services
{
    public interface IExcelFileService
    {
        Task SavePackage(string packageName, Stream xlsStream, string[] sheets);
        Task DeletePackage(string packageName);
    }    
}
