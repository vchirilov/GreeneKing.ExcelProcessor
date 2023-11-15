using Excel.Loader.WebApp.Enums;


namespace Excel.Loader.WebApp.Services
{
    public interface IImageService
    {        
        Task LoadImages(string packageName, FlowType flow, IList<byte[]> images);
    }
}
