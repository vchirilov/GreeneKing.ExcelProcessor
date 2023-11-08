using Excel.Loader.WebApp.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;

namespace Excel.Loader.WebApp.Models
{
    public class Projects: IModel
    {
        [Order(1)]
        public string ProjectName { get; set; }
        [Order(2)]
        public string PackageName { get; set; }


        public bool IsEmpty()
        {
            if (ProjectName.IsNullOrEmpty() == true
                    && PackageName.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }
    }
}
