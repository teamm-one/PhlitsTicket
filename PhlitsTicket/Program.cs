using Microsoft.AspNetCore.Identity;
using Models.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DataAccess.IRepos;
using DataAccess.Repos;
using Utility;
using Stripe;
namespace PhlitsTicket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //DbContext

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection")));

            //builder.services.addscoped

                //1-Configure Identity User&Roles
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<AirPortIRepo, AirPortRepo>();
            builder.Services.AddScoped<AirLineIRepo, AirLineRepo>();
            builder.Services.AddScoped<FlightIRepo, FlightRepo>();
            builder.Services.AddScoped<SeatIRepo, SeatRepo>();
            builder.Services.AddScoped<AirLineFlightsIRepo, AirLineFlightsRepo>();
            builder.Services.AddScoped<TripIRepo, TripRepo>();


            builder.Services.ConfigureApplicationCookie(o =>
            {
                o.LoginPath = "/User/Login";
                o.AccessDeniedPath = "/User/NotAdmin";
            });
            builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

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
