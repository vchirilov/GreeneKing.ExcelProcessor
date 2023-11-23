using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
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
            if (PackageName.IsNullOrEmpty() == true
                    && Author.IsNullOrEmpty() == true
                    && Overview.IsNullOrEmpty() == true
                    && Location.IsNullOrEmpty() == true
                    && Technology.IsNullOrEmpty() == true
                    && ChildPackages.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator Package(Packages model)
        {
            var dal = new Package();
            dal.PackageName = model.PackageName;
            dal.Author = model.Author;
            dal.Overview = model.Overview;
            dal.Location = model.Location;
            dal.Technology = model.Technology;
            dal.ChildPackages = model.ChildPackages;
            return dal;
        }

        public static explicit operator Packages(Package dal)
        {
            var model = new Packages();
            model.PackageName = dal.PackageName;
            model.Author = dal.Author;
            model.Overview = dal.Overview;
            model.Location = dal.Location;
            model.Technology = dal.Technology;
            model.ChildPackages = dal.ChildPackages;
            return model;
        }

    }
}
