using Excel.Loader.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Excel.Loader.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            await ProcessXlsFile(model.UploadXlsFile!, cancellationToken);

            return Ok();
        }

        private async Task<bool> ProcessXlsFile(IFormFile xlsFile, CancellationToken cancellationToken)
        {
            if (xlsFile == null || xlsFile.Length == 0)
            {
                return await Task.FromResult(false);
            }

            // Check the file extension. Excel files typically have .xlsx or .xls extensions
            if (!xlsFile.FileName.EndsWith(".xlsx") && !xlsFile.FileName.EndsWith(".xls"))
            {
                return await Task.FromResult(false);
            }

            var packages = new List<PackageModel>();

            using (var stream = new MemoryStream())
            {
                await xlsFile.CopyToAsync(stream, cancellationToken);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        packages.Add(new PackageModel
                        {
                            PackageName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Author = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Location = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Technology = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            ChildPackages = worksheet.Cells[row, 5].Value.ToString().Trim()
                        });
                    }
                }
            }

            return true;

        }
    }
}