namespace Jioanand.Models;

public class Document
{
    public int DocumentId { get; set; }
    public int ClientId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }

    // Navigation property
    public Client Client { get; set; } = null!;
}
