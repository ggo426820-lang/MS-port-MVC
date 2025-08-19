using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }= string.Empty;

        public string Description { get; set; }= string.Empty;

        public string LongDescription { get; set; }= string.Empty;

        [MaxLength(1000)]
        public string TechnologyStack { get; set; }= string.Empty;

        [MaxLength(500)]
        public string GitHubUrl { get; set; }= string.Empty;

        [MaxLength(500)]
        public string LiveUrl { get; set; }= string.Empty;

        [MaxLength(500)]
        public string ImageUrl { get; set; }= string.Empty;

        [MaxLength(500)]
        public string ThumbnailUrl { get; set; }= string.Empty;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }=null;

        public int Status { get; set; } = 1;
        public int DisplayOrder { get; set; } = 0;
        public bool IsFeatured { get; set; } = false;
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string CreatedBy { get; set; }= string.Empty;

        [MaxLength(100)]
        public string UpdatedBy { get; set; }= string.Empty;

        public bool IsDeleted { get; set; } = false;
        // Add to existing Project class
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}