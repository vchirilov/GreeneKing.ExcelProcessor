using Excel.Loader.WebApp.Enums;
using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Models;
using Excel.Loader.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;
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

        [HttpGet]
        public IActionResult DownloadXlsFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DownloadXlsFile(FileDownloadModel model, CancellationToken cancellationToken)
        {
            var stream = await _excelFileService.DownloadPackage(model.PackageName);

            string xlsFileName = $"{model.PackageName}_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", xlsFileName);
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

                ViewBag.PackageName += string.Format("<b>{0}</b> package has been processed and saved into database with success.<br/>", model.PackageName);

                return View("Index");
            }
            catch (ApplicationError err)
            {
                _logger.LogError(err.ToString());

                return Error();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return Error();
            }
        } 
                
        private async Task ProcessXlsFile(string packageName, IFormFile xlsFile, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
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
                    await xlsFile.CopyToAsync(stream, cancellationToken);
                    await _excelFileService.SavePackage(packageName, stream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
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