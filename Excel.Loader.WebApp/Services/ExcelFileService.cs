using Excel.Loader.WebApp.Models;
using OfficeOpenXml;

namespace Excel.Loader.WebApp.Services
{
    public class ExcelFileService : IExcelFileService
    {
        public Task SaveWorkbook(Stream xlsStream, string[] sheets)
        {
            List<Projects> dsProjects = null;
            List<Packages> dsPackages = null;
            List<Parameters> dsParameters = null;

            try
            {                
                using (var workbook = new Workbook(xlsStream, sheets))
                {
                    foreach (var worksheet in workbook.Worksheets)
                    {
                        if (worksheet.Key.Equals(nameof(Projects), StringComparison.OrdinalIgnoreCase))
                            dsProjects = Parser.Parse<Projects>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(Packages), StringComparison.OrdinalIgnoreCase))
                            dsPackages = Parser.Parse<Packages>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(Parameters), StringComparison.OrdinalIgnoreCase))
                            dsParameters = Parser.Parse<Parameters>(worksheet.Value);
                    }
                }

                return Task.CompletedTask;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
