using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BiografCSharpTest.Data;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Adgangskode skal være mellem 4 og 8 karaktere")]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }
        [Required]
        [FieldLength(8, 20)]
        public int PhoneNumber { get; set; }
        public bool Enabled { get; set; }
        public Role Role { get; set; }
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
            Enabled = true;
        }
    }
}