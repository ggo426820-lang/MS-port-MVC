using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MostafaSaidPortfolio.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }=string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; }= string.Empty;

        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }

        public string Location { get; set; }= string.Empty;
    }
}