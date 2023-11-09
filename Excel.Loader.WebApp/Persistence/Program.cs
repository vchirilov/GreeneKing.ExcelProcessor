using Excel.Loader.WebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace Excel.Loader.WebApp.Persistence
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IExcelFileService, ExcelFileService>();
            builder.Services.AddDbContext<GreeneKingContext>(options =>
            {
                var config = builder.Configuration;
                var connectionString = config.GetConnectionString("GreeneKingConnectionString");
                options.UseSqlServer(connectionString);
            });            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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

            app.Run();
        }
    }
}