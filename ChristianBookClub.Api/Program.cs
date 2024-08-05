using ChristianBookClub.Data;
using ChristianBookClub.Data.Entities;
using ChristianBookClub.Data.Interfaces;
using ChristianBookClub.Data.Repositories;
using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

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

            var options = new IdentityOptions();

            builder.Services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                })
                .AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole<long>>()
                .AddApiEndpoints()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

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
            app.MapCustomIdentityApi<User>();

            app.MapControllers();


            CreateRoles(app).Wait();
            app.Run();
        }

		private static async Task CreateRoles(WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();
				var roles = new[] { "Admin", "Usuário" };

				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
					{
						await roleManager.CreateAsync(new IdentityRole<long>(role));
					}
				}
			}
		}
	}
}
