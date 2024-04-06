using AdminPanel.Helpers;
using HubFurniture.APIs.Helpers;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Repository;
using HubFurniture.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Stripe;

namespace AdminPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole> (options => 
            {
                // Configure user validation rules
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = ""; 
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreContext>();

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            builder.Services.AddAutoMapper(typeof(MapsProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
