using System;
using System.ComponentModel.DataAnnotations;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Dtos
{
    public class ReservationForCreationDto
    {
        public int Row { get; set; }
        public int Seat { get; set; }
        public int BookingState { get; set; }
        public Show Show { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        public ReservationForCreationDto()
        {
            Created = DateTime.Now;
        }
    }
}