using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class Destinations: IModel
    {
        [Order(1)]
        public string Server { get; set; }
        [Order(2)]
        public string DatabaseOrFilePath { get; set; }
        [Order(3)]
        public string DestinationType { get; set; }
        [Order(4)]
        public string PackageName { get; set; }
       
        public bool IsEmpty()
        {
            if (Server.IsNullOrEmpty() == true
                     && DatabaseOrFilePath.IsNullOrEmpty() == true
                     && DestinationType.IsNullOrEmpty() == true
                     && PackageName.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator Destination (Destinations model)
        {
            var dal = new Destination();
            dal.Server = model.Server;
            dal.DatabaseOrFilePath = model.DatabaseOrFilePath;
            dal.DestinationType = model.DestinationType;
            dal.PackageName = model.PackageName;
    
            return dal;
        }

        public static explicit operator Destinations (Destination dal)
        {
            var model = new Destinations();
            model.Server = dal.Server;
            model.DatabaseOrFilePath = dal.DatabaseOrFilePath;
            model.DestinationType = dal.DestinationType;
            model.PackageName = dal.PackageName;

            return model;
        }
    }
}
