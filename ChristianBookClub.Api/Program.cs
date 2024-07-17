using ChristianBookClub.Data;
using ChristianBookClub.Data.Interfaces;
using ChristianBookClub.Data.Repositories;
using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ChristianBookClub.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
            });

            builder.Services
                .AddTransient<ISeminarRepository, SeminarRepository>()
                .AddTransient<ISeminarService, SeminarService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
