using Excel.Loader.WebApp.Enums;
using Excel.Loader.WebApp.Models;
using Excel.Loader.WebApp.Persistence;

namespace Excel.Loader.WebApp.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;
        GreeneKingContext _dbContext;

        public ImageService(ILogger<ImageService> logger, GreeneKingContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task LoadImages(string packageName, FlowType flow, IList<byte[]> images)
        {
            if (flow == FlowType.ControlFlow)
            {
                var controlFlows = images.Select(image => new ControlFlow { PackageName = packageName, ControlFlow1 = image }).ToList();
                _dbContext.ControlFlows.AddRange(controlFlows);
                await _dbContext.SaveChangesAsync();
            }
            else if (flow == FlowType.DataFlow)
            {
                var dataFlows = images.Select(image => new DataFlow { PackageName = packageName, DataFlow1 = image }).ToList();
                _dbContext.DataFlows.AddRange(dataFlows);
                await _dbContext.SaveChangesAsync();
            }           
        }
    }
}
