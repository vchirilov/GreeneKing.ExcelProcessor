using OfficeOpenXml;

namespace Excel.Loader.WebApp
{
    public class Workbook : IDisposable
    {
        public Workbook(Stream xlsStream, string[] sheets) => Initialize(xlsStream, sheets);

        public Dictionary<string, ExcelWorksheet> Worksheets = new Dictionary<string, ExcelWorksheet>();

        private ExcelPackage package = null;

        public void Dispose()
        {
            foreach (var worksheet in Worksheets)
                worksheet.Value?.Dispose();

            package.Dispose();
        }

        private void Initialize(Stream xlsStream, string[] sheets)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            package = new ExcelPackage(xlsStream);

            foreach (var sheet in sheets)
            {
                Worksheets.Add(sheet, package.Workbook.Worksheets[sheet]);
            }
        }
    }
}
