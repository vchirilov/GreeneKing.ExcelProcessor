using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class PackageModel
    {
        public int Id { get; set; } = 0;
        public string PackageName { get; set; }
        public string Author { get; set; }
        public string Overview { get; set; }
        public string Location { get; set; }
        public string Technology { get; set; }
        public string ChildPackages { get; set; }
    }
}
