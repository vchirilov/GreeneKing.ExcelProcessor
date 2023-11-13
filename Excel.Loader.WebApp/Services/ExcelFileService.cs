using Excel.Loader.WebApp.Models;
using Excel.Loader.WebApp.Persistence;
using System.Reflection.Metadata;

namespace Excel.Loader.WebApp.Services
{
    public class ExcelFileService : IExcelFileService
    {
        private readonly ILogger<ExcelFileService> _logger;

        GreeneKingContext _dbContext;

        List<Projects> dsProjects = null;
        List<Packages> dsPackages = null;
        List<Parameters> dsParameters = null;
        List<Sources> dsSources = null;
        List<Destinations> dsDestinations = null;
        List<Models.SourceTransformation> dsSourceTransformations = null;
        List<Models.DestinationTransformation> dsDestinationTransformation = null;
        List<Mappings> dsMappings = null;
        List<Executables> dsExecutables = null;
        List<Jobs> dsJobs = null;
        List<JobHistory> dsJobHistory = null;

        public ExcelFileService(ILogger<ExcelFileService> logger, GreeneKingContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task SaveWorkbook(string packageName, Stream xlsStream, string[] sheets)
        {
            await ExtractDataFromXlsFile(xlsStream, sheets);

            //await DeletePackage(packageName);

            await SavePackages(); //Important to be the first statement as Package object is parent entity
            await SaveProjects();
            await SaveParameters();
            await SaveSources();
            await SaveDestinations();
        }

        private async Task DeletePackage(string packageName)
        {
            var dbPackage = _dbContext.Packages.FirstOrDefault(p => p.PackageName == packageName);
            var dbControlFlows = _dbContext.ControlFlows.Where(p => p.PackageName == packageName);
            var dbDataFlows = _dbContext.DataFlows.Where(p => p.PackageName == packageName);

            if (dbPackage != null)
            {                
                _dbContext.ControlFlows.RemoveRange(dbControlFlows);
                _dbContext.DataFlows.RemoveRange(dbDataFlows);
                _dbContext.Packages.Remove(dbPackage);

                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SavePackages()
        {
            var dbPackages = dsPackages.Select(
                p => new Package
                {
                    PackageName = p.PackageName,
                    Author = p.Author,
                    Overview = p.Overview,
                    Location = p.Location,
                    Technology = p.Technology,
                    ChildPackages = p.ChildPackages
                });

            _dbContext.Packages.AddRange(dbPackages);

            await _dbContext.SaveChangesAsync();
        }

        private async Task SaveProjects()
        {
            var dbProjects = dsProjects.Select(
                p => new Project
                {
                    PackageName = p.PackageName,
                   ProjectName = p.ProjectName
                });

            _dbContext.Projects.AddRange(dbProjects);

            await _dbContext.SaveChangesAsync();
        }

        private async Task SaveParameters()
        {
            var dbParameters = dsParameters.Select(
                p => new PackageParameter
                {
                    PackageName = p.PackageName,
                    ParameterName = p.ParameterName,
                    ParameterType = p.ParameterType
                });

            _dbContext.PackageParameters.AddRange(dbParameters);

            await _dbContext.SaveChangesAsync();
        }

        private async Task SaveSources()
        {
            var dbSources = dsSources.Select(
                p => new Source
                {
                    Server = p.Server,
                    DatabaseOrFilePath = p.DatabaseOrFilePath,
                    SourceType = p.SourceType,
                    PackageName = p.PackageName
                });

            _dbContext.Sources.AddRange(dbSources);

            await _dbContext.SaveChangesAsync();
        }

        private async Task SaveDestinations()
        {
            var dbDestinations = dsDestinations.Select(
                p => new Destination
                {
                    Server = p.Server,
                    DatabaseOrFilePath = p.DatabaseOrFilePath,
                    DestinationType = p.DestinationType,
                    PackageName = p.PackageName
                });

            _dbContext.Destinations.AddRange(dbDestinations);

            await _dbContext.SaveChangesAsync();
        }

        private Task ExtractDataFromXlsFile(Stream xlsStream, string[] sheets)
        {
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

                        if (worksheet.Key.Equals(nameof(Models.SourceTransformation), StringComparison.OrdinalIgnoreCase))
                            dsSourceTransformations = Parser.Parse<Models.SourceTransformation>(worksheet.Value);

                        if (worksheet.Key.Equals(nameof(Models.DestinationTransformation), StringComparison.OrdinalIgnoreCase))
                            dsDestinationTransformation = Parser.Parse<Models.DestinationTransformation>(worksheet.Value);

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
                throw;
            }
        }
    }
}
