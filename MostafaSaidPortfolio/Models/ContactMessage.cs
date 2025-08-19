using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }= string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; }= string.Empty;

        [Required, MaxLength(2000)]
        public string Message { get; set; }= string.Empty;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}