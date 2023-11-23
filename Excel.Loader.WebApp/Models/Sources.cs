using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class Sources: IModel
    {
        [Order(1)]
        public string Server { get; set; }
        [Order(2)]
        public string DatabaseOrFilePath { get; set; }
        [Order(3)]
        public string SourceType { get; set; }
        [Order(4)]
        public string PackageName { get; set; }
       
        public bool IsEmpty()
        {
            if (Server.IsNullOrEmpty() == true
                    && DatabaseOrFilePath.IsNullOrEmpty() == true
                    && SourceType.IsNullOrEmpty() == true
                    && PackageName.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator Source(Sources model)
        {
           return new Source 
           { 
                Server = model.Server, 
                DatabaseOrFilePath = model.DatabaseOrFilePath, 
                SourceType = model.SourceType, 
                PackageName = model.PackageName 
            };
        }

        public static explicit operator Sources(Source dal)
        {
            return new Sources 
            { 
                Server = dal.Server, 
                DatabaseOrFilePath = dal.DatabaseOrFilePath, 
                SourceType = dal.SourceType, 
                PackageName = dal.PackageName 
            };
        }

    }
}
