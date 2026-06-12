using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [ForeignKey("BlogPost")]
        public int? BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public User? Author { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
    }
}
