using System;

namespace BiografCSharpTest.Dtos
{
    public class ReservationForCreation
    {
        public DateTime Created { get; set; }
        public ReservationForCreation()
        {
            Created = DateTime.Now;
        }
    }
}