using Excel.Loader.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public async Task<IActionResult> UploadFiles(FileUploadModel model)
        {
            //if (file == null || file.Length == 0)
            //{
            //    // Handle the case where no file was selected
            //    return View();
            //}

            //// Check the file extension (Excel files typically have .xlsx or .xls extensions)
            //if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
            //{
            //    // Handle the case where an unsupported file type is selected
            //    return View();
            //}

            //// Process the file, for example, you can save it to a location or read its contents using a library like EPPlus.

            //// Example: Saving the file
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}

            // Redirect to a success page or perform other actions
            return Ok();
        }
    }
}