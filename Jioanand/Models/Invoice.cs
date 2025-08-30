namespace Jioanand.Models;

public class Invoice
{
    public int InvoiceId { get; set; }
    public int BookingId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal GSTRate { get; set; }
    public decimal GSTAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public Booking Booking { get; set; } = null!;
}
