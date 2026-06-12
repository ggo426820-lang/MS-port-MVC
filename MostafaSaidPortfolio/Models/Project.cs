using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MostafaSaidPortfolio.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string TechnologyStack { get; set; } = string.Empty;
        public string GitHubUrl { get; set; } = string.Empty;
        public string LiveUrl { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int Status { get; set; } = 1;
        public int DisplayOrder { get; set; } = 0;
        public bool IsFeatured { get; set; } = false;
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        // Populated by JOIN in Dapper queries — not a DB column
        public string? CategoryName { get; set; }

        // Navigation (not used by Dapper directly)
        public Category? Category { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public string[] TechStackList =>
            string.IsNullOrWhiteSpace(TechnologyStack)
                ? Array.Empty<string>()
                : TechnologyStack.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}
