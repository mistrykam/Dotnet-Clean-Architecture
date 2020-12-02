using App.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class Book
    {
        private List<Review> _reviews;
        private DateTime? _publishedDate;

        [Key]
        public int BookId { get; private set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; private set; }

        [Required]
        [MaxLength(256)]
        public string Author { get; private set; }

        public BookFormatType BookFormat { get; private set; }

        public DateTime? PublishedDate
        {
            get => _publishedDate;

            private set
            {
                if (value != null)
                    _publishedDate = ((DateTime)value).Date;
                else
                    _publishedDate = null;
            }
        }

        public int LikeCount { get; private set; }

        public int DislikeCount { get; private set; }

        [Required]
        public IEnumerable<Review> Reviews { get => _reviews; private set => _reviews = (List<Review>)value; }

        private Book()
        {
            _reviews = new List<Review>();
        }

        public static Book CreateBook(string title, string author, DateTime? publishedDate = null, BookFormatType bookFormat = null)
        {
            return new Book()
            {
                Title = title,
                Author = author,
                PublishedDate = publishedDate,
                BookFormat = bookFormat,
                LikeCount = 0,
                DislikeCount = 0
            };
        }

        public void Update(string title, string author, DateTime? publishedDate = null, BookFormatType bookFormat = null)
        {
            Title = title;
            Author = author;
            PublishedDate = publishedDate;
            BookFormat = bookFormat;
        }

        public void Like()
        {
            LikeCount++;
        }

        public void DisLike()
        {
            DislikeCount++;
        }

        public void AddReview(Review review)
        {
            _reviews.Add(review);
        }
    }
}