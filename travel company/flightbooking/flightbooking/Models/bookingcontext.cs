using Microsoft.EntityFrameworkCore;

namespace flightbooking.Models
{
    public class bookingcontext:DbContext 

    {
        public bookingcontext(DbContextOptions<bookingcontext> options)
            : base(options)
        {
        }

        public DbSet<user> Users { get; set; } = null!;
        public DbSet<airline> Airlines { get; set; } = null!;
        public DbSet<trip> Trips { get; set; } = null!;
        public DbSet<booking> Bookings { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<trip>()
                .HasOne(t => t.Airline)
                .WithMany()
                .HasForeignKey(t => t.AirlineId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<booking>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<booking>()
                .HasOne(b => b.Trip)
                .WithMany()
                .HasForeignKey(b => b.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<user>().HasData(
                new user { UserId = 1, Name = "yafa", Email = "yafa@mail.com", Password = "1234", Role = userRole.Customer, phonenum="12345678" },
                new user { UserId = 2, Name = "doha", Email = "doha@mail.com", Password = "1234", Role = userRole.Admin, phonenum="87654321" },
                new user { UserId = 3, Name = "ahmad", Email = "ahmad@mail.com", Password = "1234", Role = userRole.Customer, phonenum="12885678" },
                new user { UserId = 4, Name = "Diana", Email = "diana@mail.com", Password = "1234", Role = userRole.Customer, phonenum="17345675" }
            );

            modelBuilder.Entity<airline>().HasData(
                new airline { AirlineId = 1, Name = "SkyFly", Country = "USA", Rating = 4.5, Logo = "skyfly.png" },
                new airline { AirlineId = 2, Name = "AirWave", Country = "Germany", Rating = 4.2, Logo = "airwave.png" },
                new airline { AirlineId = 3, Name = "BlueWings", Country = "UK", Rating = 4.8, Logo = "bluewings.png" },
                new airline { AirlineId = 4, Name = "turkish", Country = "Spain", Rating = 4.0 }
            );

            modelBuilder.Entity<trip>().HasData(
                new trip { TripId = 1, FromCity = "New York", ToCity = "London", DepartureDateTime = DateTime.Now.AddDays(7), ArrivalDateTime = DateTime.Now.AddDays(7).AddHours(7), Price = 1200, AvailableSeats = 100, AirlineId = 1 },
                new trip { TripId = 2, FromCity = "Berlin", ToCity = "Paris", DepartureDateTime = DateTime.Now.AddDays(3), ArrivalDateTime = DateTime.Now.AddDays(3).AddHours(2), Price = 350, AvailableSeats = 50, AirlineId = 2 },
                new trip { TripId = 3, FromCity = "London", ToCity = "Tokyo", DepartureDateTime = DateTime.Now.AddDays(10), ArrivalDateTime = DateTime.Now.AddDays(11), Price = 2000, AvailableSeats = 80, AirlineId = 3 },
                new trip { TripId = 4, FromCity = "Madrid", ToCity = "Rome", DepartureDateTime = DateTime.Now.AddDays(5), ArrivalDateTime = DateTime.Now.AddDays(5).AddHours(3), Price = 400, AvailableSeats = 60, AirlineId = 4 },
                new trip { TripId = 5, FromCity = "Paris", ToCity = "Dubai", DepartureDateTime = DateTime.Now.AddDays(15), ArrivalDateTime = DateTime.Now.AddDays(15).AddHours(6), Price = 1500, AvailableSeats = 90, AirlineId = 2 }
            );

            modelBuilder.Entity<booking>().HasData(
                new booking { BookingId = 1, BookingDate = DateTime.Now, SeatsBooked = 2, Status = "Confirmed", UserId = 1, TripId = 1 },
                new booking { BookingId = 2, BookingDate = DateTime.Now, SeatsBooked = 1, Status = "Confirmed", UserId = 2, TripId = 2 },
                new booking { BookingId = 3, BookingDate = DateTime.Now, SeatsBooked = 3, Status = "Confirmed", UserId = 3, TripId = 3 },
                new booking { BookingId = 4, BookingDate = DateTime.Now, SeatsBooked = 2, Status = "Confirmed", UserId = 1, TripId = 4 },
                new booking { BookingId = 5, BookingDate = DateTime.Now, SeatsBooked = 1, Status = "Confirmed", UserId = 4, TripId = 5 }
            );
        }
    }
}
