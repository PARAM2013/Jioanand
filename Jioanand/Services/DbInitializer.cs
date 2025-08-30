using Jioanand.Data;
using Jioanand.Models;

namespace Jioanand.Services;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Add test location if none exists
        if (!context.Locations.Any())
        {
            var location = new Location
            {
                Name = "Main Hall",
                Address = "123 Wedding Street, City",
                Description = "Our main wedding hall location",
                CreatedAt = DateTime.UtcNow
            };
            context.Locations.Add(location);
            context.SaveChanges();

            // Add some test rooms
            var rooms = new[]
            {
                new Room
                {
                    LocationId = location.LocationId,
                    RoomNumber = "101",
                    Floor = 1,
                    Type = Models.Enums.RoomType.Standard,
                    Capacity = 100,
                    PricePerDay = 5000,
                    Description = "Standard hall for small gatherings",
                    Status = Models.Enums.RoomStatus.Available,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Room
                {
                    LocationId = location.LocationId,
                    RoomNumber = "201",
                    Floor = 2,
                    Type = Models.Enums.RoomType.Deluxe,
                    Capacity = 200,
                    PricePerDay = 8000,
                    Description = "Deluxe hall with premium amenities",
                    Status = Models.Enums.RoomStatus.Available,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Room
                {
                    LocationId = location.LocationId,
                    RoomNumber = "301",
                    Floor = 3,
                    Type = Models.Enums.RoomType.Suite,
                    Capacity = 300,
                    PricePerDay = 12000,
                    Description = "Luxury suite with panoramic views",
                    Status = Models.Enums.RoomStatus.Available,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }
    }
}
