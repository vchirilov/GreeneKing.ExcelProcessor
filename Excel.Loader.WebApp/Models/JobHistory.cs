using Excel.Loader.WebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Excel.Loader.WebApp.Models
{
    public class JobHistory: IModel
    {
        [Order(1)]
        public string JobName { get; set; }
        [Order(2)]
        public string StepName { get; set; }
        [Order(3)]
        public DateTime LastRunDateTime { get; set; } = default;
        [Order(4)]
        public TimeOnly LastRunDuration { get; set; }  = default;
        [Order(5)]
        public string PackageName { get; set; }

        public bool IsEmpty()
        {
            if (JobName.IsNullOrEmpty() == true
                    && StepName.IsNullOrEmpty() == true
                    && LastRunDateTime == default
                    && LastRunDuration == default
                    && PackageName.IsNullOrEmpty() == true)
                return true;
            else
                return false;
        }
    }
}
