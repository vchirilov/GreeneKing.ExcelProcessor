using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
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
            if (PackageName.IsNullOrEmpty() == true
                    && ExecutableName.IsNullOrEmpty() == true
                    && ExecutableType.IsNullOrEmpty() == true
                    && ExecutedOnServer.IsNullOrEmpty() == true
                    && ExecutedOnDatabase.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator Executable(Executables model)
        {
            var dal = new Executable();
            dal.PackageName = model.PackageName;
            dal.ExecutableName = model.ExecutableName;
            dal.ExecutableType = model.ExecutableType;
            dal.ExecutedOnServer = model.ExecutedOnServer;
            dal.ExecutedOnDatabase = model.ExecutedOnDatabase;
            return dal;
        }

        public static explicit operator Executables(Executable dal)
        {
            var model = new Executables();
            model.PackageName = dal.PackageName;
            model.ExecutableName = dal.ExecutableName;
            model.ExecutableType = dal.ExecutableType;
            model.ExecutedOnServer = dal.ExecutedOnServer;
            model.ExecutedOnDatabase = dal.ExecutedOnDatabase;
            return model;
        }

    }
}
