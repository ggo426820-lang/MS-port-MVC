namespace MostafaSaidPortfolio.Areas.Admin.Models;

public enum NotificationType { Info, Success, Warning, Error }

public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; } = NotificationType.Info;
    public bool IsRead { get; set; }
    public string? Link { get; set; }
    public string? IconPath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReadAt { get; set; }
    public string? RecipientUserId { get; set; }
}
