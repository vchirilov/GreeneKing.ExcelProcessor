namespace Presentation.Services
{
    public class ExcelProcessorService : IExcelProcessorService
    {
        public Task PrcessFile(Stream xlsFile)
        {
            return Task.CompletedTask;
        }
    }
}
