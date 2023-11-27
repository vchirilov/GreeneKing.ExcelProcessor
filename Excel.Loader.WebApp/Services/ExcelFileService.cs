using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Models;
using Excel.Loader.WebApp.Options;
using Excel.Loader.WebApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System.Reflection.Metadata;

namespace Excel.Loader.WebApp.Services
{
    public class ExcelFileService : IExcelFileService
    {
        private readonly ILogger<ExcelFileService> _logger;
        private readonly ApplicationOptions _options;
        private readonly GreeneKingContext _dbContext;

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

        public ExcelFileService(ILogger<ExcelFileService> logger, GreeneKingContext dbContext, IOptions<ApplicationOptions> options)
        {
            _logger = logger;
            _dbContext = dbContext;
            _options = options.Value;
        }

        public async Task SavePackage(string packageName, Stream xlsStream)
        {            
            await ExtractDataFromXlsFile(xlsStream);
            await PersistPackage();
        }

        private async Task PersistPackage()
        {
            await LoadPackage(); //Important to be the first statement as Package object is parent entity
            await LoadProjects();
            await LoadParameters();
            await LoadSources();
            await LoadDestinations();
            await LoadSourceTransformations();
            await LoadDestinationTransformations();
            await LoadMappings();
            await LoadExecutables();
            await LoadJobs();
            await LoadJobHistory();

            await _dbContext.SaveChangesAsync();

            async Task LoadPackage()
            {
                var dalPackages = dsPackages.Select(p => (Package)p);
                await _dbContext.Packages.AddRangeAsync(dalPackages);
            }

            async Task LoadProjects()
            {
                var dalProjects = dsProjects.Select(p => (Project)p);
                await _dbContext.Projects.AddRangeAsync(dalProjects);
            }

            async Task LoadParameters()
            {
                var dalPackageParameters = dsParameters.Select(p => (PackageParameter)p);
                await _dbContext.PackageParameters.AddRangeAsync(dalPackageParameters);
            }

            async Task LoadSources()
            {
                var dalSources = dsSources.Select(s => (Source)s);
                await _dbContext.Sources.AddRangeAsync(dalSources);
            }

            async Task LoadDestinations()
            {
                var dalDestinations = dsDestinations.Select(d => (Destination)d);
                await _dbContext.Destinations.AddRangeAsync(dalDestinations);
            }

            async Task LoadSourceTransformations()
            {
                var dalSourceDestinations = dsSourceTransformations.Select(p => (Persistence.SourceTransformation)p);
                await _dbContext.SourceTransformations.AddRangeAsync(dalSourceDestinations);
            }

            async Task LoadDestinationTransformations()
            {
                var dalDestinationTransformations = dsDestinationTransformation.Select(dt => (Persistence.DestinationTransformation)dt);
                await _dbContext.DestinationTransformations.AddRangeAsync(dalDestinationTransformations);
            }

            async Task LoadMappings()
            {
                var dalMappings = dsMappings.Select(m => (Mapping)m);
                await _dbContext.Mappings.AddRangeAsync(dalMappings);
            }

            async Task LoadExecutables()
            {
                var dalExecutables = dsExecutables.Select(exe => (Executable)exe);
                await _dbContext.Executables.AddRangeAsync(dalExecutables);
            }

            async Task LoadJobs()
            {
                var dalJobs = dsJobs.Select(j => (Job)j);
                await _dbContext.Jobs.AddRangeAsync(dalJobs);
            }

            async Task LoadJobHistory()
            {
                var dalJobHistory = dsJobHistory.Select(j => (JobsHistory)j);
                await _dbContext.JobsHistories.AddRangeAsync(dalJobHistory);
            }
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
            var destinationTransformations = await GetDestinationTransformation(packageName);
            var mappings = await GetMappings(packageName);
            var executables = await GetExecutables(packageName);
            var jobs = await GetJobs(packageName);
            var jobHistories = await GetJobHistory(packageName);

            var stream = new MemoryStream();

            using (ExcelPackage xlsPackage = new(stream))
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

                var destinationTransformationWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Models.DestinationTransformation));
                destinationTransformationWorksheet.Cells.LoadFromCollection(destinationTransformations, true);

                var mappingsWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Mappings));
                mappingsWorksheet.Cells.LoadFromCollection(mappings, true);

                var executablesWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Executables));
                executablesWorksheet.Cells.LoadFromCollection(executables, true);

                var jobsWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(Jobs));
                jobsWorksheet.Cells.LoadFromCollection(jobs, true);

                var jobHistoriesWorksheet = xlsPackage.Workbook.Worksheets.Add(nameof(JobHistory));
                jobHistoriesWorksheet.Cells.LoadFromCollection(jobHistories, true);

                xlsPackage.Save();
            }
            stream.Position = 0;

            return stream;

            async Task<List<Projects>> GetProjects(string packageName)
            {
                var dbProjects = await _dbContext.Projects.Where(p => p.PackageName == packageName).ToListAsync();
                return dbProjects.Select(p => (Projects)p).ToList();
            }

            async Task<List<Packages>> GetPackages(string packageName)
            {
                var dalPackages = await _dbContext.Packages.Where(p => p.PackageName == packageName).ToListAsync();
                return dalPackages.Select(p => (Packages)p).ToList();
            }

            async Task<List<Parameters>> GetParameters(string packageName)
            {
                var dalParameters = await _dbContext.PackageParameters.Where(p => p.PackageName == packageName).ToListAsync();
                return dalParameters.Select(p => (Parameters)p).ToList();
            }

            async Task<List<Sources>> GetSources(string packageName)
            {
                var dbSources = await _dbContext.Sources.Where(p => p.PackageName == packageName).ToListAsync();
                return dbSources.Select(p => (Sources)p).ToList();
            }

            async Task<List<Destinations>> GetDestinations(string packageName)
            {
                var dalDestinations = await _dbContext.Destinations.Where(p => p.PackageName == packageName).ToListAsync();
                return dalDestinations.Select(d => (Destinations)d).ToList();
            }

            async Task<List<Models.SourceTransformation>> GetSourceDestinations(string packageName)
            {
                var dbSourceDestinations = await _dbContext.SourceTransformations.Where(p => p.PackageName == packageName).ToListAsync();
                return dbSourceDestinations.Select(p => (Models.SourceTransformation)p).ToList();
            }

            async Task<List<Models.DestinationTransformation>> GetDestinationTransformation(string packageName)
            {
                var dalDestinationTransformation = await _dbContext.DestinationTransformations.Where(p => p.PackageName == packageName).ToListAsync();
                return dalDestinationTransformation.Select(p => (Models.DestinationTransformation)p).ToList();
            }

            async Task<List<Mappings>> GetMappings(string packageName)
            {
                var dalMappings = await _dbContext.Mappings.Where(p => p.PackageName == packageName).ToListAsync();
                return dalMappings.Select(p => (Mappings)p).ToList();
            }

            async Task<List<Executables>> GetExecutables(string packageName)
            {
                var dalExecutables = await _dbContext.Executables.Where(p => p.PackageName == packageName).ToListAsync();
                return dalExecutables.Select(p => (Executables)p).ToList();
            }

            async Task<List<Jobs>> GetJobs(string packageName)
            {
                var dalJobs = await _dbContext.Jobs.Where(p => p.PackageName == packageName).ToListAsync();
                return dalJobs.Select(p => (Jobs)p).ToList();
            }

            async Task<List<JobHistory>> GetJobHistory(string packageName)
            {
                var dalJobHistory = await _dbContext.JobsHistories.Where(p => p.PackageName == packageName).ToListAsync();
                return dalJobHistory.Select(p => (JobHistory)p).ToList();
            }
        }

        

        private Task ExtractDataFromXlsFile(Stream xlsStream)
        {
            try
            {                
                using (var workbook = new Workbook(xlsStream, _options.Sheets))
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
            catch (ApplicationError err)
            {
                throw err;
            }
            catch (Exception exc)
            {
                throw ApplicationError.Create(exc);
            }
        }        
    }
}
