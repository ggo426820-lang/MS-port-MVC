using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Data.UnitOfWork;

namespace MostafaSaidPortfolio.Areas.Admin.Components.ViewComponents;

public class AdminStatsViewComponent : ViewComponent
{
    private readonly IUnitOfWork _uow;

    public AdminStatsViewComponent(IUnitOfWork uow) => _uow = uow;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var stats = new Dictionary<string, int>
        {
            ["Blog Posts"]   = await _uow.Blogs.CountPublishedAsync(),
            ["Projects"]     = await _uow.Projects.CountActiveAsync(),
            ["Messages"]     = await _uow.ContactMessages.CountAsync(),
            ["Subscribers"]  = await _uow.Newsletter.CountAsync(),
            ["Skills"]       = await _uow.Skills.CountAsync(),
        };
        return View("~/Areas/Admin/Views/Shared/Components/AdminStats/Default.cshtml", stats);
    }
}
