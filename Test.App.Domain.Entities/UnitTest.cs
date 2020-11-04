using App.Domain.Entities;
using System;
using Xunit;

namespace Test.App.Domain.Entities
{
    public class UnitTest
    {
        [Fact]
        public void Test_CreateBook1()
        {
            Book book = Book.CreateBook("Four Hour Work Week", "Tim Ferris", new DateTime(2001, 10, 12));

            book.Like();
            book.Like();

            book.DisLike();

            book.AddReview(Review.CreateReview("Great Book!", "jennifer.famil@gmail.com"));
        }

        [Fact]
        public void Test_CreateBook2()
        {
            Book book = Book.CreateBook("Brave New Work", "Tommy Jones");

            book.Like();
            book.Like();

            book.DisLike();

            book.AddReview(Review.CreateReview("Great Book!", "jennifer.famil@gmail.com"));
        }
    }
}
