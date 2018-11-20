using BiografCSharpTest.Models;

namespace BiografCSharpTest.Dtos
{
    public class UserForUpdateByAdminDto
    {
        public string Username { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}