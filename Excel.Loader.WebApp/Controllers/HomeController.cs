using Excel.Loader.WebApp.Enums;
using Excel.Loader.WebApp.Models;
using Excel.Loader.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace Excel.Loader.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExcelFileService _excelFileService;
        private readonly IImageService _imageService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IExcelFileService excelFileService, IImageService imageService)
        {
            _logger = logger;
            _excelFileService = excelFileService;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> UploadFiles(FileUploadModel model, CancellationToken cancellationToken)
        {
            try
            {
                await DeletePackage(model.PackageName);

                await ProcessXlsFile(model.PackageName, model.UploadXlsFile!, cancellationToken);
                await ProcessImageFile(model.PackageName, FlowType.ControlFlow, model.UploadControlFlowImages, cancellationToken);
                await ProcessImageFile(model.PackageName, FlowType.DataFlow, model.UploadDataFlowImages, cancellationToken);

                return Ok();
            }
            catch (Exception)
            {
                return Error();
            }
  
        } 
                
        private async Task ProcessXlsFile(string packageName, IFormFile xlsFile, CancellationToken cancellationToken)
        {
            try
            {
                if (xlsFile == null || xlsFile.Length == 0)
                {
                    await Task.FromResult(false);
                }

                // Check the file extension. Excel files typically have .xlsx or .xls extensions
                if (!xlsFile.FileName.EndsWith(".xlsx") && !xlsFile.FileName.EndsWith(".xls"))
                {
                    await Task.FromResult(false);
                }

                var packages = new List<Packages>();

                using var stream = new MemoryStream();
                var sheets = "Projects,Packages,Parameters,Sources,Destinations,SourceTransformation,DestinationTransformation,Mappings,Executables,Jobs,JobHistory".Split(',');

                await xlsFile.CopyToAsync(stream, cancellationToken);
                await _excelFileService.SavePackage(packageName, stream, sheets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task ProcessImageFile(string packageName, FlowType flowType, List<IFormFile> imageFiles, CancellationToken cancellationToken)
        {
            try
            {
                var images = new List<byte[]>();

                if (imageFiles == null || imageFiles.Count == 0)
                {
                    await Task.FromResult(false);
                }

                foreach (var imageFile in imageFiles)
                {
                    using var stream = new MemoryStream();
                    await imageFile.CopyToAsync(stream, cancellationToken);
                    images.Add(stream.ToArray());
                }

                await _imageService.LoadImages(packageName, flowType, images);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task DeletePackage(string packageName)
        {
            await _excelFileService.DeletePackage(packageName);
        }
    }
}