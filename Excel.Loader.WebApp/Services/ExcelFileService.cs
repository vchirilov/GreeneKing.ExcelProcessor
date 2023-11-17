using Excel.Loader.WebApp.Models;
using Excel.Loader.WebApp.Persistence;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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

        public async Task SavePackage(string packageName, Stream xlsStream, string[] sheets)
        {            
            await ExtractDataFromXlsFile(xlsStream, sheets);
            await SavePackage();
        }

        private async Task SavePackage()
        {
            await LoadPackage(); //Important to be the first statement as Package object is parent entity
            await LoadProjects();
            await LoadParameters();
            await LoadSources();
            await LoadDestinations();
            await LoadSourceDestinations();
            await LoadDestinationTransformations();
            await LoadMappings();
            await LoadExecutables();
            await LoadJobs();
            await LoadJobHistory();

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePackage(string packageName)
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

        public async Task<Stream> DownloadPackage(string packageName)
        {
            var projects = await GetProjects(packageName);
            var packages = await GetPackages(packageName);
            var parameters = await GetParameters(packageName);
            var sources = await GetSources(packageName);
            var destinations = await GetDestinations(packageName);
            var sourceTransformations = await GetSourceDestinations(packageName);


            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlsPackage = new ExcelPackage(stream))
            {
                var projectWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Projects));
                projectWorksheet.Cells.LoadFromCollection(projects, true);

                var packagesWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Packages));
                packagesWorksheet.Cells.LoadFromCollection(packages, true);

                var parametersWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Parameters));
                parametersWorksheet.Cells.LoadFromCollection(packages, true);

                var sourcessWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Sources));
                sourcessWorksheet.Cells.LoadFromCollection(sources, true);

                var destinationsWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Destinations));
                destinationsWorksheet.Cells.LoadFromCollection(destinations, true);

                var sourceTransformationWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Models.SourceTransformation));
                sourceTransformationWorksheet.Cells.LoadFromCollection(sourceTransformations, true);

                xlsPackage.Save();
            }
            stream.Position = 0;

            return stream;
        }

        private Task LoadPackage()
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
            return Task.CompletedTask;
        }

        private Task LoadProjects()
        {
            var dbProjects = dsProjects.Select(
                p => new Project
                {
                    PackageName = p.PackageName,
                   ProjectName = p.ProjectName
                });

            _dbContext.Projects.AddRange(dbProjects);
            return Task.CompletedTask;
        }

        private Task LoadParameters()
        {
            var dbParameters = dsParameters.Select(
                p => new PackageParameter
                {
                    PackageName = p.PackageName,
                    ParameterName = p.ParameterName,
                    ParameterType = p.ParameterType
                });

            _dbContext.PackageParameters.AddRange(dbParameters);
            return Task.CompletedTask;
        }

        private Task LoadSources()
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
            return Task.CompletedTask;
        }

        private Task LoadDestinations()
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
            return Task.CompletedTask;
        }

        private Task LoadSourceDestinations()
        {
            var dbSourceDestinations = dsSourceTransformations.Select(
                p => new Persistence.SourceTransformation
                {
                    Server = p.Server,
                    DatabaseOrFilePath = p.DatabaseOrFilePath,
                    TableName = p.TableName,
                    ColumnName = p.ColumnName,
                    Read = p.Read,
                    Write = p.Write,
                    PackageName = p.PackageName
                });

            _dbContext.SourceTransformations.AddRange(dbSourceDestinations);
            return Task.CompletedTask;
        }

        private Task LoadDestinationTransformations()
        {
            var dbSourceTransformations = dsDestinationTransformation.Select(
                p => new Persistence.DestinationTransformation
                {
                    Server = p.Server,
                    DatabaseOrFilePath = p.DatabaseOrFilePath,
                    TableName = p.TableName,
                    ColumnName = p.ColumnName,
                    Read = p.Read,
                    Write = p.Write,
                    PackageName = p.PackageName
                });

            _dbContext.DestinationTransformations.AddRange(dbSourceTransformations);
            return Task.CompletedTask;
        }

        private Task LoadMappings()
        {
            var dbMappings = dsMappings.Select(
                p => new Mapping
                {
                    SourceServer = p.SourceServer,
                    SourceDatabase = p.SourceDatabase,
                    SourceTable = p.SourceTable,
                    SourceTableColumn = p.SourceTableColumn,
                    Transformation = p.Transformation,
                    DestinationServer = p.DestinationServer,
                    DestinationDatabase = p.DestinationDatabase,
                    DestinationTable = p.DestinationTable, 
                    DestinationTableColumn = p.DestinationTableColumn,                   
                    PackageName = p.PackageName
                });

            _dbContext.Mappings.AddRange(dbMappings);
            return Task.CompletedTask;
        }

        private Task LoadExecutables()
        {
            var dbExecutables = dsExecutables.Select(
                p => new Executable
                {                    
                    PackageName = p.PackageName,
                    ExecutableName = p.ExecutableName,
                    ExecutableType = p.ExecutableType,
                    ExecutedOnServer = p.ExecutedOnServer,
                    ExecutedOnDatabase = p.ExecutedOnDatabase
                });

            _dbContext.Executables.AddRange(dbExecutables);
            return Task.CompletedTask;
        }

        private Task LoadJobs()
        {
            var dbJobs = dsJobs.Select(
                p => new Job
                {
                    JobName = p.JobName,
                    Frequency = p.Frequency,
                    LastUsed = p.LastUsed,
                    PackageName = p.PackageName
                });

            _dbContext.Jobs.AddRange(dbJobs);
            return Task.CompletedTask;
        }

        private Task LoadJobHistory()
        {
            var dbJobHistory = dsJobHistory.Select(
                p => new JobsHistory
                {
                    JobName = p.JobName,
                    StepName = p.StepName,
                    LastRunDateTime = p.LastRunDateTime,
                    LastRunDuration = p.LastRunDuration.ToTimeSpan(),
                    PackageName = p.PackageName
                });

            _dbContext.JobsHistories.AddRange(dbJobHistory);
            return Task.CompletedTask;
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
            catch (Exception)
            {
                throw;
            }
        }
        
        private async Task<List<Projects>> GetProjects(string packageName)
        {
            var dbProjects = await _dbContext.Projects.Where(p => p.PackageName == packageName).ToListAsync();

            return dbProjects.Select(p => new Projects { PackageName = p.PackageName, ProjectName = p.ProjectName }).ToList();
        }

        private async Task<List<Packages>> GetPackages(string packageName)
        {
            var dbPackages = await _dbContext.Packages.Where(p => p.PackageName == packageName).ToListAsync();

            return dbPackages.Select(p => new Packages 
            { 
                PackageName = p.PackageName, 
                Author = p.Author, 
                Location = p.Location, 
                Overview = p.Overview, 
                Technology = p.Technology, 
                ChildPackages = p.ChildPackages }
            ).ToList();
        }

        private async Task<List<Parameters>> GetParameters(string packageName)
        {
            var dbParameters = await _dbContext.PackageParameters.Where(p => p.PackageName == packageName).ToListAsync();

            return dbParameters.Select(p => new Parameters 
            { 
                PackageName = p.PackageName, 
                ParameterName = p.ParameterName, 
                ParameterType = p.ParameterType }
            ).ToList();
        }

        private async Task<List<Sources>> GetSources(string packageName)
        {
            var dbSources = await _dbContext.Sources.Where(p => p.PackageName == packageName).ToListAsync();

            return dbSources.Select(p => new Sources
            {
                Server = p.Server,
                DatabaseOrFilePath = p.DatabaseOrFilePath,
                SourceType = p.SourceType,
                PackageName = p.PackageName
            }
            ).ToList();
        }

        private async Task<List<Destinations>> GetDestinations(string packageName)
        {
            var dsDestinations = await _dbContext.Destinations.Where(p => p.PackageName == packageName).ToListAsync();

            return dsDestinations.Select(p => new Destinations
            {
                Server = p.Server,
                DatabaseOrFilePath = p.DatabaseOrFilePath,
                DestinationType = p.DestinationType,
                PackageName = p.PackageName
            }
            ).ToList();
        }

        private async Task<List<Models.SourceTransformation>> GetSourceDestinations(string packageName)
        {
            var dbSourceDestinations = await _dbContext.SourceTransformations.Where(p => p.PackageName == packageName).ToListAsync();

            return dbSourceDestinations.Select(p => new Models.SourceTransformation
            {
                Server = p.Server,
                DatabaseOrFilePath = p.DatabaseOrFilePath,
                TableName = p.TableName,
                ColumnName = p.ColumnName,
                Read = p.Read,
                Write = p.Write,
                PackageName = packageName                
            }
            ).ToList();
        }
    }
}
