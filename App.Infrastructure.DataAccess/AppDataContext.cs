using App.Domain.Application.Framework.Interfaces;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace App.Infrastructure.DataAccess
{
    public class AppDataContext : DbContext, IAppDataContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId = 9, CategoryName = "Toys" });

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 6, ProductName = "Under Runner", UnitPrice = 59.95M, Discontinued = false, CategoryId = 1 });
            */
        }
    }

    /// <summary>
    /// See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation#from-a-design-time-factory
    /// 
    ///      Install-Package Microsoft.Extensions.Configuration.Json 
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDataContext>
    {
        public AppDataContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", optional: false);

            var configuration = configBuilder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<AppDataContext>();
            builder.UseSqlServer(connectionString);

            return new AppDataContext(builder.Options);
        }
    }
}
