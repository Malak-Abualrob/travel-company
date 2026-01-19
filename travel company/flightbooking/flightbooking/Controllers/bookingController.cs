using flightbooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace flightbooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly bookingcontext _context;

        public BookingController(bookingcontext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> Create(int tripId)
        {
            var trip = await _context.Trips
                .Include(t => t.Airline)
                .FirstOrDefaultAsync(t => t.TripId == tripId);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int tripId, int seatsBooked)
        {
            var trip = await _context.Trips.FindAsync(tripId);
            if (trip == null)
                return NotFound();

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("SignIn", "User"); 

            if (seatsBooked <= 0 || seatsBooked > trip.AvailableSeats)
            {
                ViewBag.Error = "incorrect seat number!";
                return View(trip);
            }

            var booking = new booking
            {
                TripId = tripId,
                UserId = userId.Value,
                SeatsBooked = seatsBooked,
                Status = "Confirmed"
            };

           
            trip.AvailableSeats -= seatsBooked;

            _context.Bookings.Add(booking);
            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyBookings");
        }

       
        public async Task<IActionResult> MyBookings()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("SignIn", "User");

            var bookings = await _context.Bookings
                .Include(b => b.Trip)
                .ThenInclude(t => t.Airline)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return View(bookings);
        }

       
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Trip)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
                return NotFound();

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || booking.UserId != userId)
                return Unauthorized();

           
            booking.Trip.AvailableSeats += booking.SeatsBooked;

            _context.Bookings.Remove(booking);
            _context.Trips.Update(booking.Trip);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyBookings");
        }
    }
}