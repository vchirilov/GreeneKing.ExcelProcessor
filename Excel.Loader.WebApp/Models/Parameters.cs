using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
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
            if (PackageName.IsNullOrEmpty() == true
                    && ParameterName.IsNullOrEmpty() == true
                    && ParameterType.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator PackageParameter(Parameters model)
        {
            var dal = new PackageParameter();
            dal.PackageName = model.PackageName;
            dal.ParameterName = model.ParameterName;
            dal.ParameterType = model.ParameterType;
            return dal;
        }

        public static explicit operator Parameters(PackageParameter dal)
        {
            var model = new Parameters();
            model.PackageName = dal.PackageName;
            model.ParameterName = dal.ParameterName;
            model.ParameterType = dal.ParameterType;
            return model;
        }

    }
}
