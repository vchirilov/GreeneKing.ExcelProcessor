namespace Presentation.Models
{
    public class FileUploadModel
    {
        public string PackageName { get; set; } = string.Empty;
        public List<IFormFile> UploadedFiles { get; set; } = new List<IFormFile>();

    }
}
