﻿using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; private set; }

        [Required]
        [MaxLength(1024)]
        public string Comment { get; private set; }

        [Required]
        [MaxLength(128)]
        public string EmailAddress { get; private set; }

        [Required]
        public bool Flagged { get; private set; } = false;

        [Required]
        public DateTime CreatedDateTime { get; private set; }

        private Review()
        {
        }

        public static Review CreateReview(string comment, string emailAddress)
        {
            return new Review()
            {
                Comment = comment,
                EmailAddress = emailAddress,
                CreatedDateTime = DateTime.Now
            };
        }
    }
}