using System.ComponentModel.DataAnnotations;

namespace flightbooking.Models
{
    public enum userRole
    {
        Customer=0,
        Admin=1
    }
    public class user
    {

        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public userRole Role { get; set; } = userRole.Customer;
        public string phonenum { get; set; } 

    }
}
