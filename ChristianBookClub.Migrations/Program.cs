using ChristianBookClub.Data;
using Microsoft.EntityFrameworkCore;

namespace ChristianBookClub.Migrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("Database"), sqliteOptions =>
                {
                    sqliteOptions.MigrationsAssembly("ChristianBookClub.Migrations");
                });
            });

            var host = builder.Build();
            host.Run();
        }
    }
}