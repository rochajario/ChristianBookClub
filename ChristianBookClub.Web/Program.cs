using ChristianBookClub.Data;
using ChristianBookClub.Data.Entities;
using ChristianBookClub.Data.Interfaces;
using ChristianBookClub.Data.Repositories;
using ChristianBookClub.Domain.Extensions;
using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Services;
using ChristianBookClub.Web.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChristianBookClub.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services
                .AddTransient<ISeminarRepository, SeminarRepository>()
                .AddTransient<ISeminarService, SeminarService>()
                .AddTransient<ICertificateService, CertificateService>()
                .AddFluentEMail(builder.Configuration)
                .AddTransient<IEmailService, EmailService>()
                .AddTransient<IEmailSender, EmailSender>();


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}
