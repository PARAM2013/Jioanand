namespace Jioanand.Models;

public class Location
{
    public int LocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
