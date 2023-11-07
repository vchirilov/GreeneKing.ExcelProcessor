namespace Excel.Loader.WebApp.Helpers
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        public OrderAttribute(int key = 0)
        {
            Key = key;
        }

        public int Key { get; }
    }
}
