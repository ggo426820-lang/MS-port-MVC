using System;
using System.ComponentModel.DataAnnotations;

namespace MostafaSaidPortfolio.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Author { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Company { get; set; } = string.Empty;

        public int Rating { get; set; } = 5;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
