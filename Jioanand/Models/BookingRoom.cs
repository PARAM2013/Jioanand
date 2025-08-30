namespace Jioanand.Models;

public class BookingRoom
{
    public int BookingRoomId { get; set; }
    public int BookingId { get; set; }
    public int RoomId { get; set; }
    public decimal PricePerDay { get; set; }
    public decimal SubTotal { get; set; }

    // Navigation properties
    public Booking Booking { get; set; } = null!;
    public Room Room { get; set; } = null!;
}
