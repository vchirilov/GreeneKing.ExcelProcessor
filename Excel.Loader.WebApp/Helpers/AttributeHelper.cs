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

        public static IOrderedEnumerable<PropertyInfo> GetSortedProperties(object obj)
        {
            return obj.GetType()
              .GetProperties()
              .OrderBy(p => ((OrderAttribute)p.GetCustomAttribute(typeof(OrderAttribute), false)).Key);
        }

        public static PropertyInfo GetPropertyByKey<T>(int orderKey) => typeof(T).GetProperties()
            .Where(p => ((OrderAttribute)p.GetCustomAttribute(typeof(OrderAttribute), false)).Key.Equals(orderKey))
            .FirstOrDefault();

        public static PropertyInfo GetPropertyByKey(Type type, int orderKey) => type.GetProperties()
            .Where(p => ((OrderAttribute)p.GetCustomAttribute(typeof(OrderAttribute), false)).Key.Equals(orderKey))
            .FirstOrDefault();

    }
}
