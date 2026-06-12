using MostafaSaidPortfolio.Areas.Admin.Models;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetForUserAsync(string userId, bool unreadOnly = false);
    Task<int> CountUnreadAsync(string userId);
    Task SendAsync(string recipientUserId, string title, string message,
        NotificationType type = NotificationType.Info, string? link = null);
    Task MarkReadAsync(Guid id);
    Task MarkAllReadAsync(string userId);
    Task DeleteAsync(Guid id);
}
