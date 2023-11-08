using Excel.Loader.WebApp.Helpers;
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
    }
}
