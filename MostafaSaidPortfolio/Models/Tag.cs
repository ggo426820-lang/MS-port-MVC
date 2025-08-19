using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }= string.Empty;

        public ICollection<Project> Projects { get; set; }= new List<Project>();
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}