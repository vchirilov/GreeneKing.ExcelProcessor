﻿using Excel.Loader.WebApp.Helpers;
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
    }
}