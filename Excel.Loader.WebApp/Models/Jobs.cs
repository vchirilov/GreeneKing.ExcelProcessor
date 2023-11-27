using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Persistence;
using OfficeOpenXml.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class Jobs: IModel
    {
        [Order(1)]
        public string JobName { get; set; }
        [Order(2)]
        public string Frequency { get; set; }
        [Order(3)]
        [EpplusTableColumn(NumberFormat = "MM/dd/yyyy HH:mm:ss")]
        public DateTime LastUsed { get; set; } = default;
        [Order(4)]
        public string PackageName { get; set; }

        public bool IsEmpty()
        {
            if (JobName.IsNullOrEmpty() == true
                   && Frequency.IsNullOrEmpty() == true
                   && LastUsed == default
                   && PackageName.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }

        public static explicit operator Job(Jobs model)
        {
            return new Job 
            {
                JobName = model.JobName,
                Frequency = model.Frequency,
                LastUsed = model.LastUsed,
                PackageName = model.PackageName
            };
        }

        public static explicit operator Jobs(Job dal)
        {
            return new Jobs 
            {
                JobName = dal.JobName,
                Frequency = dal.Frequency,
                LastUsed = dal.LastUsed ?? default, 
                PackageName = dal.PackageName
            };
        }

    }
}
