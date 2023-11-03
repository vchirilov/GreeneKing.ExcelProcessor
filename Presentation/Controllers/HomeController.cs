using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Services;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExcelProcessorService _excelProcessorService;

        public HomeController(ILogger<HomeController> logger, IExcelProcessorService excelProcessorService)
        {
            _logger = logger;
            _excelProcessorService = excelProcessorService;
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



        public async Task<IActionResult> UploadFiles(FileUploadModel model)
        {

            if (model is null || string.IsNullOrWhiteSpace(model.PackageName) || model?.UploadedFiles?.Count == 0)
            {
                return View();
            }

            // Check the file extension (Excel files typically have .xlsx or .xls extensions)
            if (model?.UploadedFiles is not null && !model.UploadedFiles.Any(file => file.FileName.EndsWith(".xlsx")))
            {
                // Handle the case where an unsupported file type is selected
                return View();
            }

            var xlsFile = GetXlsFile(model);

            // Process the file, for example, you can save it to a location or read its contents using a library like EPPlus.
            await _excelProcessorService.PrcessFile(xlsFile.OpenReadStream());

            return Ok();
        }

        private IFormFile GetXlsFile(FileUploadModel model) => model.UploadedFiles.FirstOrDefault(file => file.FileName.EndsWith(".xlsx"));
        
    }
}