using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Components.ViewComponents;

public class AdminMenuViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string? currentController = null)
    {
        ViewBag.CurrentController = currentController ?? RouteData.Values["controller"]?.ToString() ?? "";
        return View("~/Areas/Admin/Views/Shared/Components/AdminMenu/Default.cshtml");
    }
}
