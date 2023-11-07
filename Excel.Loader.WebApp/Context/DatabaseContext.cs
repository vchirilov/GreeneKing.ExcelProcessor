using Excel.Loader.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Excel.Loader.WebApp.Context
{
    public class DatabaseContext: DbContext
    {        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = string.Format(@"Data Source=51.12.52.30;Initial Catalog=Students;Persist Security Info=True;User ID=sa;Password=spartak_1");
            optionsBuilder.UseSqlServer(connectionString);
        }      

        public DbSet<Student> Students { get; set; }

    }
}
