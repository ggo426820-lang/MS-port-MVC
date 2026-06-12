using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MostafaSaidPortfolio.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string MetaTitle { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string FeaturedImageUrl { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public int Status { get; set; } = 0;
        public DateTime? PublishedAt { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public int ViewCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int? ReadingTime { get; set; }
        public bool IsFeatured { get; set; } = false;
        public bool IsPublished { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        // Populated by JOIN in Dapper queries — not DB columns
        public string? CategoryName { get; set; }
        public string? AuthorName { get; set; }

        // Navigation (not used by Dapper directly)
        public Category? Category { get; set; }
        public User? Author { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public int EstimatedReadingTime =>
            ReadingTime ?? Math.Max(1, Content.Split(' ').Length / 200);
    }
}
