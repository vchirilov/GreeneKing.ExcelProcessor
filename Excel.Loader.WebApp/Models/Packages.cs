using Excel.Loader.WebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class Packages: IModel
    {
        [Order(1)]
        public string PackageName { get; set; }
        [Order(2)]
        public string Author { get; set; }
        [Order(3)]
        public string Overview { get; set; }
        [Order(4)]
        public string Location { get; set; }
        [Order(5)]
        public string Technology { get; set; }
        [Order(6)]
        public string ChildPackages { get; set; }

        public bool IsEmpty()
        {
            return false;
        }
    }
}
