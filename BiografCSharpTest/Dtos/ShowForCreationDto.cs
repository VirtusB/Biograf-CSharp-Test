using System;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Dtos
{
    public class ShowForCreationDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TicketPrice { get; set; }
        public int HallNumber { get; set; }
        public Movie Movie { get; set; }
    }
}