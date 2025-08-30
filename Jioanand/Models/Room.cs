using Jioanand.Models.Enums;

namespace Jioanand.Models;

public class Room
{
    public int RoomId { get; set; }
    public int LocationId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int Floor { get; set; }
    public RoomType Type { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerDay { get; set; }
    public string? Description { get; set; }
    public RoomStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public Location Location { get; set; } = null!;
    public ICollection<BookingRoom> BookingRooms { get; set; } = new List<BookingRoom>();
}
