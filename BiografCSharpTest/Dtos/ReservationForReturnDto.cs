using System;

namespace BiografCSharpTest.Dtos
{
    public class ReservationForReturnDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public int BookingState { get; set; }
    }
}