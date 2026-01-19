using System.ComponentModel.DataAnnotations;

namespace flightbooking.Models
{
    public class airline
    {
        [Key]
        public int AirlineId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string? Country { get; set; }
        public double? Rating { get; set; }
    }
}
