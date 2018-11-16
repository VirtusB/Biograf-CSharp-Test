using System;

namespace BiografCSharpTest.Dtos
{
    public class ReservationForCreationDto
    {
        public DateTime Created { get; set; }
        public ReservationForCreationDto()
        {
            Created = DateTime.Now;
        }
    }
}