using Excel.Loader.WebApp.Options;
using Excel.Loader.WebApp.Persistence;
using Excel.Loader.WebApp.Services;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Excel.Loader.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IExcelFileService, ExcelFileService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(ApplicationOptions.ApplicationOptionsConfigKey));
            builder.Services.AddDbContext<GreeneKingContext>(options =>
            {
                var config = builder.Configuration;
                var connectionString = config.GetConnectionString("GreeneKingConnectionString");
                options.UseSqlServer(connectionString);
            });            

            var app = builder.Build();

            var loggerFactory = app.Services.GetService<ILoggerFactory>();
            loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());


            // Configure the HTTP request pipeline.
            app.UseExceptionHandler("/Home/Error");

            if (!app.Environment.IsDevelopment())
            {                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            app.Run();
        }
    }
}