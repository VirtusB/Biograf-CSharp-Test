using System;
using System.ComponentModel.DataAnnotations;

namespace BiografCSharpTest.Models
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TicketPrice { get; set; }
        public int HallNumber { get; set; }
        [Required]
        public Movie Movie { get; set; }
    }
}