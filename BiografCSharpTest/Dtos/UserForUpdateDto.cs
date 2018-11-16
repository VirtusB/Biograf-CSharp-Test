using System.ComponentModel.DataAnnotations;
using BiografCSharpTest.Helpers;

namespace BiografCSharpTest.Dtos
{
    public class UserForUpdateDto
    {
        [Required]
        public string City { get; set; } 
        [Required]
        public string Country { get; set; } 
        [Required]
        [FieldLength(8, 20)]
        public int PhoneNumber { get; set; }
    }
}