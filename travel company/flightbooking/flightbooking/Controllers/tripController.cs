using flightbooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace flightbooking.Controllers
{
    public class TripController : Controller
    {
        private readonly bookingcontext _context;

        public TripController(bookingcontext context)
        {
            _context = context;
        }

    
        public async Task<IActionResult> Index(string? fromCity, string? toCity)
        {
            var trips = _context.Trips.Include(t => t.Airline).AsQueryable();

            if (!string.IsNullOrEmpty(fromCity))
                trips = trips.Where(t => t.FromCity.Contains(fromCity));

            if (!string.IsNullOrEmpty(toCity))
                trips = trips.Where(t => t.ToCity.Contains(toCity));

            return View(await trips.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int id)
        {
            var trip = await _context.Trips
                .Include(t => t.Airline)
                .FirstOrDefaultAsync(t => t.TripId == id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Trips.Update(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
                return NotFound();

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}