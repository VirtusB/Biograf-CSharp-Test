using System;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime Created { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public Role Role { get; set; }
    }
}