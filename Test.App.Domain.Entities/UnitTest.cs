using App.Domain.Entities;
using App.Domain.Entities.Enum;
using App.Domain.Entities.Framework;
using System;
using System.Collections.Generic;
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

        [Fact]
        public void Test_GetEnumeration_BookFormatType()
        {
            IReadOnlyDictionary<string, BookFormatType> list = Enumeration.GetAll<BookFormatType>();

            foreach (KeyValuePair<string, BookFormatType> item in list)
            {
                System.Diagnostics.Debug.WriteLine($"{item.Key} {item.Value.Value} {item.Value.DisplayName}");
            }
        }
    }
}
