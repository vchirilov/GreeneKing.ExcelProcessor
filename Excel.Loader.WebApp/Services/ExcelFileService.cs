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
            List<Sources> dsSources = null;
            List<Destinations> dsDestinations = null; 
            List<SourceTransformation> dsSourceTransformations = null;
            List<DestinationTransformation> dsDestinationTransformation = null;
            List<Mappings> dsMappings = null;
            List<Executables> dsExecutables = null;
            List<Jobs> dsJobs = null;
            List<JobHistory> dsJobHistory = null;

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

                        if (worksheet.Key.Equals(nameof(Sources), StringComparison.OrdinalIgnoreCase))
                            dsSources = Parser.Parse<Sources>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(Destinations), StringComparison.OrdinalIgnoreCase))
                            dsDestinations = Parser.Parse<Destinations>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(SourceTransformation), StringComparison.OrdinalIgnoreCase))
                            dsSourceTransformations = Parser.Parse<SourceTransformation>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(DestinationTransformation), StringComparison.OrdinalIgnoreCase))
                            dsDestinationTransformation = Parser.Parse<DestinationTransformation>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(Mappings), StringComparison.OrdinalIgnoreCase))
                            dsMappings = Parser.Parse<Mappings>(worksheet.Value);


                        if (worksheet.Key.Equals(nameof(Executables), StringComparison.OrdinalIgnoreCase))
                            dsExecutables = Parser.Parse<Executables>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(Jobs), StringComparison.OrdinalIgnoreCase))
                            dsJobs = Parser.Parse<Jobs>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(JobHistory), StringComparison.OrdinalIgnoreCase))
                            dsJobHistory = Parser.Parse<JobHistory>(worksheet.Value);
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
