using Excel.Loader.WebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class DestinationTransformation: IModel
    {
        [Order(1)]
        public string Server { get; set; }
        [Order(2)]
        public string DatabaseOrFilePath { get; set; }
        [Order(3)]
        public string TableName { get; set; }
        [Order(4)]
        public string ColumnName { get; set; }
        [Order(5)]
        public string Read { get; set; }
        [Order(6)]
        public string Write { get; set; }
        [Order(7)]
        public string PackageName { get; set; }

        public bool IsEmpty()
        {
            if (Server.IsNullOrEmpty() == true
                    && DatabaseOrFilePath.IsNullOrEmpty() == true
                    && TableName.IsNullOrEmpty() == true
                    && ColumnName.IsNullOrEmpty() == true
                    && Read.IsNullOrEmpty() == true
                    && Write.IsNullOrEmpty() == true
                    && PackageName.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator Persistence.DestinationTransformation(DestinationTransformation model)
        {
            var dal = new Persistence.DestinationTransformation();
            dal.Server = model.Server;
            dal.DatabaseOrFilePath = model.DatabaseOrFilePath;
            dal.TableName = model.TableName;
            dal.ColumnName = model.ColumnName;
            dal.Read = model.Read;
            dal.Write = model.Write;
            dal.PackageName = model.PackageName;
            return dal;
        }

        public static explicit operator DestinationTransformation(Persistence.DestinationTransformation dal)
        {
            var model = new DestinationTransformation();
            model.Server = dal.Server;
            model.DatabaseOrFilePath = dal.DatabaseOrFilePath;
            model.TableName = dal.TableName;
            model.ColumnName = dal.ColumnName;
            model.Read = dal.Read;
            model.Write = dal.Write;
            model.PackageName = dal.PackageName;
            return model;
        }
    }
}
