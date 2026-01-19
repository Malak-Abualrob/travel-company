using System.ComponentModel.DataAnnotations;

namespace flightbooking.Models
{
    public class trip
    {

        [Key]
        public int TripId { get; set; }

        [Required]
        public string FromCity { get; set; } = string.Empty;

        [Required]
        public string ToCity { get; set; } = string.Empty;

        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }

        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }

        public int AirlineId { get; set; }
        public airline? Airline { get; set; }
    }
}
