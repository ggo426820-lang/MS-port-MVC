using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}