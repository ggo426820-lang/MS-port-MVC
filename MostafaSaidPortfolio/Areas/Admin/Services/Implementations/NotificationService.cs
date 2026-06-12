using MostafaSaidPortfolio.Areas.Admin.Models;
using MostafaSaidPortfolio.Areas.Admin.Services.Interfaces;

namespace MostafaSaidPortfolio.Areas.Admin.Services.Implementations;

public class NotificationService : INotificationService
{
    private static readonly List<Notification> _notifications = new();
    private static readonly object _lock = new();

    public Task<IEnumerable<Notification>> GetForUserAsync(string userId, bool unreadOnly = false)
    {
        lock (_lock)
        {
            var q = _notifications.Where(n => n.RecipientUserId == userId || n.RecipientUserId == null);
            if (unreadOnly) q = q.Where(n => !n.IsRead);
            return Task.FromResult<IEnumerable<Notification>>(q.OrderByDescending(n => n.CreatedAt).ToList());
        }
    }

    public Task<int> CountUnreadAsync(string userId)
    {
        lock (_lock)
        {
            return Task.FromResult(_notifications.Count(n =>
                !n.IsRead && (n.RecipientUserId == userId || n.RecipientUserId == null)));
        }
    }

    public Task SendAsync(string recipientUserId, string title, string message,
        NotificationType type = NotificationType.Info, string? link = null)
    {
        var n = new Notification
        {
            Title           = title,
            Message         = message,
            Type            = type,
            Link            = link,
            RecipientUserId = recipientUserId,
            CreatedAt       = DateTime.UtcNow
        };
        lock (_lock) { _notifications.Insert(0, n); }
        return Task.CompletedTask;
    }

    public Task MarkReadAsync(Guid id)
    {
        lock (_lock)
        {
            var n = _notifications.FirstOrDefault(x => x.Id == id);
            if (n != null) { n.IsRead = true; n.ReadAt = DateTime.UtcNow; }
        }
        return Task.CompletedTask;
    }

    public Task MarkAllReadAsync(string userId)
    {
        lock (_lock)
        {
            foreach (var n in _notifications.Where(x => !x.IsRead &&
                (x.RecipientUserId == userId || x.RecipientUserId == null)))
            {
                n.IsRead = true; n.ReadAt = DateTime.UtcNow;
            }
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        lock (_lock) { _notifications.RemoveAll(x => x.Id == id); }
        return Task.CompletedTask;
    }
}
