using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace flightbooking.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    phonenum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    AirlineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeatsBooked = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airlines",
                columns: new[] { "AirlineId", "Country", "Logo", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "USA", "skyfly.png", "SkyFly", 4.5 },
                    { 2, "Germany", "airwave.png", "AirWave", 4.2000000000000002 },
                    { 3, "UK", "bluewings.png", "BlueWings", 4.7999999999999998 },
                    { 4, "Spain", null, "turkish", 4.0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "Password", "Role", "phonenum" },
                values: new object[,]
                {
                    { 1, "yafa@mail.com", "yafa", "1234", 0, "12345678" },
                    { 2, "doha@mail.com", "doha", "1234", 1, "87654321" },
                    { 3, "ahmad@mail.com", "ahmad", "1234", 0, "12885678" },
                    { 4, "diana@mail.com", "Diana", "1234", 0, "17345675" }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "TripId", "AirlineId", "ArrivalDateTime", "AvailableSeats", "DepartureDateTime", "FromCity", "Price", "ToCity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 25, 21, 39, 17, 550, DateTimeKind.Local).AddTicks(2952), 100, new DateTime(2026, 1, 25, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(2906), "New York", 1200m, "London" },
                    { 2, 2, new DateTime(2026, 1, 21, 16, 39, 17, 550, DateTimeKind.Local).AddTicks(2959), 50, new DateTime(2026, 1, 21, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(2957), "Berlin", 350m, "Paris" },
                    { 3, 3, new DateTime(2026, 1, 29, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(2964), 80, new DateTime(2026, 1, 28, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(2962), "London", 2000m, "Tokyo" },
                    { 4, 4, new DateTime(2026, 1, 23, 17, 39, 17, 550, DateTimeKind.Local).AddTicks(2969), 60, new DateTime(2026, 1, 23, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(2967), "Madrid", 400m, "Rome" },
                    { 5, 2, new DateTime(2026, 2, 2, 20, 39, 17, 550, DateTimeKind.Local).AddTicks(2974), 90, new DateTime(2026, 2, 2, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(2972), "Paris", 1500m, "Dubai" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "BookingDate", "SeatsBooked", "Status", "TripId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 18, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(3014), 2, "Confirmed", 1, 1 },
                    { 2, new DateTime(2026, 1, 18, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(3019), 1, "Confirmed", 2, 2 },
                    { 3, new DateTime(2026, 1, 18, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(3023), 3, "Confirmed", 3, 3 },
                    { 4, new DateTime(2026, 1, 18, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(3027), 2, "Confirmed", 4, 1 },
                    { 5, new DateTime(2026, 1, 18, 14, 39, 17, 550, DateTimeKind.Local).AddTicks(3031), 1, "Confirmed", 5, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TripId",
                table: "Bookings",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_AirlineId",
                table: "Trips",
                column: "AirlineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Airlines");
        }
    }
}
