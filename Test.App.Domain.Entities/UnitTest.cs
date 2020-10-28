using App.Domain.Entities;
using System;
using Xunit;

namespace Test.App.Domain.Entities
{
    public class UnitTest
    {
        [Fact]
        public void Test_CreateBook()
        {
            Book book = Book.CreateBook("Four Hour Work Week", "Tim Ferris", 2001, 10, 12);

            book.Like();
            book.Like();

            book.DisLike();

            book.AddReview(Review.CreateReview("Great Book!", "jennifer.famil@gmail.com"));
        }
    }
}
