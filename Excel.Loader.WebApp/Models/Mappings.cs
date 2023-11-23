using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class Mappings: IModel
    {
        [Order(1)]
        public string SourceServer { get; set; }
        [Order(2)]
        public string SourceDatabase { get; set; }
        [Order(3)]
        public string SourceTable { get; set; }
        [Order(4)]
        public string SourceTableColumn { get; set; }
        [Order(5)]
        public string Transformation { get; set; }
        [Order(6)]
        public string DestinationServer { get; set; }
        [Order(7)]
        public string DestinationDatabase { get; set; }
        [Order(8)]
        public string DestinationTable { get; set; }
        [Order(9)]
        public string DestinationTableColumn { get; set; }
        [Order(10)]
        public string PackageName { get; set; }

        public bool IsEmpty()
        {
            if (SourceServer.IsNullOrEmpty() == true
                    && SourceDatabase.IsNullOrEmpty() == true
                    && SourceTable.IsNullOrEmpty() == true
                    && SourceTableColumn.IsNullOrEmpty() == true
                    && Transformation.IsNullOrEmpty() == true
                    && DestinationServer.IsNullOrEmpty() == true
                    && DestinationDatabase.IsNullOrEmpty() == true
                    && DestinationTable.IsNullOrEmpty() == true
                    && DestinationTableColumn.IsNullOrEmpty() == true
                    && PackageName.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator Mapping(Mappings model)
        {
            return new Mapping 
            {
                SourceServer = model.SourceServer,
                SourceDatabase = model.SourceDatabase,
                SourceTable = model.SourceTable,
                SourceTableColumn = model.SourceTableColumn,
                Transformation = model.Transformation,
                DestinationServer = model.DestinationServer,
                DestinationDatabase = model.DestinationDatabase,
                DestinationTable = model.DestinationTable,
                DestinationTableColumn = model.DestinationTableColumn,
                PackageName = model.PackageName
            };
        }

        public static explicit operator Mappings(Mapping dal)
        {
            return new Mappings 
            {
                SourceServer = dal.SourceServer,
                SourceDatabase = dal.SourceDatabase,
                SourceTable = dal.SourceTable,
                SourceTableColumn = dal.SourceTableColumn,
                Transformation = dal.Transformation,
                DestinationServer = dal.DestinationServer,
                DestinationDatabase = dal.DestinationDatabase,
                DestinationTable = dal.DestinationTable,
                DestinationTableColumn = dal.DestinationTableColumn,
                PackageName = dal.PackageName
            };
        }

    }
}
