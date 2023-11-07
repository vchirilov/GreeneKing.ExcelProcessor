namespace Excel.Loader.WebApp.Helpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelAttribute : Attribute
    {
        public string Table { get; set; }
    }
}
