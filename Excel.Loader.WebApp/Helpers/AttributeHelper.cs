using System.Reflection;

namespace Excel.Loader.WebApp.Helpers
{
    public static class AttributeHelper
    {
        public static IOrderedEnumerable<PropertyInfo> GetSortedProperties<T>()
        {
            return typeof(T)
              .GetProperties()
              .OrderBy(p => ((OrderAttribute)p.GetCustomAttribute(typeof(OrderAttribute), false)).Key);
        }
    }
}
