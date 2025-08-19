using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }=string.Empty;
        // Add to existing User class
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}