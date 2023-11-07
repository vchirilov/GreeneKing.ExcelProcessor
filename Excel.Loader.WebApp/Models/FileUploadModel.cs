namespace Excel.Loader.WebApp.Models
{
    public class FileUploadModel
    {
        public string PackageName { get; set; } = string.Empty;
        public IFormFile? UploadXlsFile { get; set; }
        public List<IFormFile> UploadControlFlowImages { get; set; } = new List<IFormFile>();
        public List<IFormFile> UploadDataFlowImages { get; set; } = new List<IFormFile>();
    }
}
