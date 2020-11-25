using App.Domain.Interfaces;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using App.Domain.Entities.Enum;
using App.Domain.Entities.Framework;

namespace App.Infrastructure.DataAccess
{
    public class AppDataContext : DbContext, IAppDataContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BookFormatType> BookFormats { get; set; }

        public AppDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
            modelBuilder.Entity<Book>()
                .OwnsOne(x => x.BookFormat)
                .Property(x => x.Value);
            */
                   
            // BookFormatType Enum mapping
            modelBuilder.Entity<BookFormatType>().HasKey(x => x.Value  );
            modelBuilder.Entity<BookFormatType>().Property(x => x.Value).HasDefaultValue(1).ValueGeneratedNever().IsRequired();
            modelBuilder.Entity<BookFormatType>().Property(x => x.DisplayName).IsRequired();

            // Seed BookFormatType
            modelBuilder.Entity<BookFormatType>().HasData(
                BookFormatType.Book,
                BookFormatType.AudioBook,
                BookFormatType.EBook);
        
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
