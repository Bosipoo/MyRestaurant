using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.Areas.Identity.Data;
using MyRestaurant.Data;

[assembly: HostingStartup(typeof(MyRestaurant.Areas.Identity.IdentityHostingStartup))]
namespace MyRestaurant.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MyRestaurantContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MyRestaurantContextConnection")));

                //services.AddEntityFrameworkSqlite().AddDbContext<MyRestaurantContext>();


                services.AddDefaultIdentity<MyRestaurantUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<MyRestaurantContext>();
            });

            //using (var client = new MyRestaurantContext())
            //{
            //    client.Database.EnsureCreated();
            //}
        }
    }
}