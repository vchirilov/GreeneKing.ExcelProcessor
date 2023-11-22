using Excel.Loader.WebApp.Models;
using OfficeOpenXml;

namespace Excel.Loader.WebApp.Services
{
    public interface IExcelFileService
    {
        Task SavePackage(string packageName, Stream xlsStream);
        Task DeletePackage(string packageName);
        Task<Stream> DownloadPackage(string packageName);
    }    
}
