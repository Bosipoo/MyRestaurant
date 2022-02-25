using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Areas.Identity.Data;
using MyRestaurant.Models;

namespace MyRestaurant.Data
{
    public class MyRestaurantContext : IdentityDbContext<MyRestaurantUser>
    {
        public MyRestaurantContext(DbContextOptions<MyRestaurantContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Data Source=sql2016.fse.network;Initial Catalog=db_1914741_myrestaurant;User ID=user_db_1914741_myrestaurant;Password=Pizz_6t7");
        //    optionsBuilder.UseSqlite("Filename=Restaurant.db");
        //}

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUser(builder);
            this.SeedRole(builder);
        }

        private void SeedUser(ModelBuilder builder)
        {
            MyRestaurantUser user = new MyRestaurantUser()
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Administrator",
                PhoneNumber = "09090549692",
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            PasswordHasher<MyRestaurantUser> passwordHasher = new PasswordHasher<MyRestaurantUser>();

            passwordHasher.HashPassword(user, "Admin*123");

            builder.Entity<MyRestaurantUser>().HasData(user);
        }

        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "c325f92e-c0d6-4e74-9eae-c38394ae00c9", NormalizedName = "Admin" }
                );
        }
    }
}
