using Excel.Loader.WebApp.Helpers;
using Excel.Loader.WebApp.Models;
using OfficeOpenXml;
using System.Globalization;
using static Excel.Loader.WebApp.Helpers.Utility;

namespace Excel.Loader.WebApp.Services
{
    public static class Parser
    {
        public static List<T> Parse<T>(ExcelWorksheet worksheet) where T : IModel, new()
        {
            var sheet = typeof(T).Name;
            var data = new List<T>();

            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            //Fetch data from spreadsheet file
            for (int row = 2; row <= rowCount; row++)
            {
                T obj = new T();
                var col = 1;

                foreach (var prop in AttributeHelper.GetSortedProperties<T>())
                {
                    object value = worksheet.Cells[row, col].Value;
                    string textValue = worksheet.Cells[row, col].Text;

                    try
                    {
                        switch (prop.PropertyType.Name)
                        {
                            case "Int32":
                                value = Convert.ToInt32(value);
                                break;
                            case "Decimal":
                                value = Convert.ToDecimal(value);
                                break;
                            case "String":
                                value = Convert.ToString(value);
                                break;
                            case "DateTime":
                                value = Convert.ToDateTime(value);
                                break;
                            case "TimeOnly":
                                value = TimeOnly.FromDateTime(Convert.ToDateTime(value));
                                break;
                            default:
                                break;
                        }

                        typeof(T).GetProperty($"{prop.Name}").SetValue(obj, textValue.IsNullOrEmpty() ? null : value);
                        col++;
                    }
                    catch
                    {
                        throw ApplicationError.Create($"Page [{sheet}], column [{prop.Name}]. Can't convert value [{value}] to type {prop.PropertyType.Name}");
                    }
                }

                if (!obj.IsEmpty())
                    data.Add(obj);
            }

            return data;
        }        
    }
}
