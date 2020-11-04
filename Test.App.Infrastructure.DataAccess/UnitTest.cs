using App.Domain.Entities;
using App.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace Test.App.Infrastructure.DataAccess
{
    public class UnitTest
    {
        [Fact]
        public async void TestAsync()
        {
            using (AppDataContext dbContext = new AppDataContext(GetConnectionDetails()))
            {
                Book book = Book.CreateBook("Four Hour Work Week", "Tim Ferris", new DateTime(2001, 10, 12));

                book.Like();
                book.Like();

                book.DisLike();

                book.AddReview(Review.CreateReview("Great Book!", "jennifer.famil@gmail.com"));

                await dbContext.Books.AddAsync(book);

                await dbContext.SaveChangesAsync();
            }
        }

        private static DbContextOptions GetConnectionDetails()
        {
            var configBuilder = new ConfigurationBuilder();

            configBuilder.AddJsonFile("appsettings.json", optional: false);

            var configuration = configBuilder.Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();

            optionsBuilder.UseSqlServer(connectionString);

            return optionsBuilder.Options;
        }
    }
}
