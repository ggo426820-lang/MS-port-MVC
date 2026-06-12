using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Areas.Admin.Models;

namespace MostafaSaidPortfolio.Areas.Admin.Components.ViewComponents;

public class NotificationsWidgetViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IEnumerable<Notification>? notifications = null, int unreadCount = 0)
    {
        ViewBag.UnreadCount    = unreadCount;
        ViewBag.Notifications  = notifications ?? Enumerable.Empty<Notification>();
        return View("~/Areas/Admin/Views/Shared/Components/NotificationsWidget/Default.cshtml");
    }
}
