using System.Diagnostics;
using System.Text;

namespace Excel.Loader.WebApp.Helpers
{
    public class ApplicationError : Exception
    {
        private ApplicationError(string origin, string method, string text)
        {
            CallerClass = origin;
            CallerMethod = method;
            Text = text;
        }

        public static ApplicationError Create(string text)
        {
            StackTrace stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(1).GetMethod();
            var methodName = methodBase.Name;
            var className = methodBase.ReflectedType.Name;

            return new ApplicationError(className, methodName, text);
        }

        public static ApplicationError Create(Exception exc)
        {
            return Create(exc.Message);
        }

        public override string ToString()
        {
            StringBuilder text = new ();
            text.AppendLine($"Class:{CallerClass}");
            text.AppendLine($"Method:{CallerMethod}");
            text.AppendLine($"StackTrace:{StackTrace}");
            text.AppendLine($"Error Message: {Text}");
            return text.ToString();
        }
        
        public string Text { get; set; }
        private string CallerClass { get; set; }
        private string CallerMethod { get; set; }
        
    }
}
