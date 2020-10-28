using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class Book : IValidatableObject
    {
        private List<Review> _reviews;

        [Key]
        public int BookId { get; private set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; private set; }

        [Required]
        [MaxLength(256)]
        public string Author { get; private set; }

        public DateTime? PublishedDate { get; private set; }

        public int LikeCount { get; private set; }

        public int DislikeCount { get; private set; }

        [Required]
        [MaxLength(256)]
        public IEnumerable<Review> Reviews { get => _reviews; private set => _reviews = (List<Review>)value; }

        private Book()
        {
            _reviews = new List<Review>();
        }

        public static Book CreateBook(string title, string author, int year, int month, int day)
        {
            return new Book()
            {
                Title = title,
                Author = author,
                PublishedDate = new DateTime(year, month, day)
            };
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            return errors;
        }
    }
}