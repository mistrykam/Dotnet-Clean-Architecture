using System;
using System.ComponentModel;

namespace App.Domain.Application.Features.Books.Queries
{
    /// <summary>
    /// Return Book View Model
    /// </summary>
    public class BookViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [DisplayName("Published Date")]
        public DateTime? PublishedDate { get; set; }

        [DisplayName("Like Count")]
        public int LikeCount { get; set; }

        [DisplayName("Dislike Count")]
        public int DislikeCount { get; set; }
    }
}