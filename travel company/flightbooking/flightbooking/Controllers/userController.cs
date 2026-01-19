using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightbooking.Models;
namespace flightbooking.Controllers
{
    public class UserController : Controller
    {
        private readonly bookingcontext _context;

        public UserController(bookingcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(user user)
        {
            if (ModelState.IsValid)
            {
               
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ViewBag.Error = "this email is already used!";
                    return View(user);
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

               
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

       
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "email or password is incorrect";
            return View();
        }

        
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
    }
}