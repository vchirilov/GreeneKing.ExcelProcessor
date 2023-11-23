using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
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

        public static explicit operator Project (Projects model)
        {
            var dal = new Project();
            dal.ProjectName = model.ProjectName;
            dal.PackageName = model.PackageName;
            return dal;
        }

        public static explicit operator Projects(Project dal)
        {
            var model = new Projects();
            model.ProjectName = dal.ProjectName;
            model.PackageName = dal.PackageName;
            return model;
        }
    }
}
