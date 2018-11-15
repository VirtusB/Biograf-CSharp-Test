using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BiografCSharpTest.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}