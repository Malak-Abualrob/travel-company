using System.ComponentModel.DataAnnotations;

namespace flightbooking.Models
{
    public class booking
    {
        [Key]
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;
        public int SeatsBooked { get; set; }
        public string Status { get; set; } = "Confirmed";

        public int UserId { get; set; }
        public user? User { get; set; }

        public int TripId { get; set; }
        public trip? Trip { get; set; }
    }
}
