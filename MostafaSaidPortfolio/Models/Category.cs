using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }= string.Empty;

        [MaxLength(500)]
        public string Description { get; set; }= string.Empty;

        [MaxLength(100)]
        public string Slug { get; set; }= string.Empty;

        [MaxLength(100)]
        public string Icon { get; set; }=string.Empty;

        [MaxLength(20)]
        public string Color { get; set; }=string.Empty;

        [MaxLength(20)]
        public string BackgroundColor { get; set; }= string.Empty;

        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        [ForeignKey("ParentCategory")]
        public int? ParentId { get; set; }
        public Category ParentCategory { get; set; }= null;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Project> Projects { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}