using Excel.Loader.WebApp.Helpers;
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
    }
}
