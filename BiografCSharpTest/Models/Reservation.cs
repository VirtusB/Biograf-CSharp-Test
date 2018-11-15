using System;
using System.ComponentModel.DataAnnotations;

namespace BiografCSharpTest.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public int BookingState { get; set; }
        [Required]
        public Show Show { get; set; }
        [Required]
        public User User { get; set; }
    }
}