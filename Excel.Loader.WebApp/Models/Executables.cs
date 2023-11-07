using Excel.Loader.WebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class Executables: IModel
    {
        [Order(1)]
        public string PackageName { get; set; }
        [Order(2)]
        public string ExecutableName { get; set; }
        [Order(3)]
        public string ExecutableType { get; set; }
        [Order(4)]
        public string ExecutedOnServer { get; set; }
        [Order(5)]
        public string ExecutedOnDatabase { get; set; }

        public bool IsEmpty()
        {
            return false;
        }
    }
}
