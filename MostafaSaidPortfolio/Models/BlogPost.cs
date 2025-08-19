using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // add this


        [Required]
        [MaxLength(300)]
        public string Title { get; set; }= string.Empty;

        [Required]
        public string Content { get; set; }

        [MaxLength(1000)]
        public string Summary { get; set; }

        [MaxLength(200)]
        public string Slug { get; set; }

        [MaxLength(200)]
        public string MetaTitle { get; set; }

        [MaxLength(500)]
        public string MetaDescription { get; set; }

        [MaxLength(500)]
        public string? FeaturedImageUrl { get; set; } = string.Empty; // required
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public User Author { get; set; }

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
        // Navigation property
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        // Add to existing BlogPost class
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

}