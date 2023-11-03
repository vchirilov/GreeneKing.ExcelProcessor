namespace Presentation.Services
{
    public interface IExcelProcessorService
    {
        Task PrcessFile(Stream xlsFile);
    }
}
