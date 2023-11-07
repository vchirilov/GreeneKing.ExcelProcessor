using Excel.Loader.WebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class Parameters: IModel
    {
        [Order(1)]
        public string PackageName { get; set; }
        [Order(2)]
        public string ParameterName { get; set; }
        [Order(3)]
        public string ParameterType { get; set; }

        public bool IsEmpty()
        {
            return false;
        }
    }
}
