using Excel.Loader.WebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class SourceTransformation: IModel
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

        public static explicit operator Persistence.SourceTransformation(SourceTransformation model)
        {
            return new Persistence.SourceTransformation 
            {
                Server = model.Server,
                DatabaseOrFilePath = model.DatabaseOrFilePath,
                TableName = model.TableName,
                ColumnName = model.ColumnName,
                Read = model.Read,
                Write = model.Write,
                PackageName = model.PackageName
            };
        }

        public static explicit operator SourceTransformation(Persistence.SourceTransformation dal)
        {
            return new SourceTransformation 
            {
                Server = dal.Server,
                DatabaseOrFilePath = dal.DatabaseOrFilePath,
                TableName = dal.TableName,
                ColumnName = dal.ColumnName,
                Read = dal.Read,
                Write = dal.Write,
                PackageName = dal.PackageName
            };
        }

    }
}
