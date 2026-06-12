using System;
using System.ComponentModel.DataAnnotations;

namespace MostafaSaidPortfolio.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        public bool IsOnline { get; set; } = false;
        public int MaxAttendees { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
