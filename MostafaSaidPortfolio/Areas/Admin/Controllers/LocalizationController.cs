using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LocalizationController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"]      = "Localization";
            ViewData["Breadcrumb"] = "Admin / Localization";
            var languages = new[] { ("en", "English", true), ("ar", "Arabic (العربية)", true) };
            ViewData["Languages"] = languages;
            return View("~/Areas/Admin/Views/Localization/Index.cshtml");
        }

        public IActionResult Edit(string culture)
        {
            ViewData["Title"]   = $"Edit Strings — {culture}";
            ViewData["Culture"] = culture;
            return View("~/Areas/Admin/Views/Localization/Edit.cshtml");
        }

        public IActionResult AddLanguage()
        {
            ViewData["Title"] = "Add Language";
            return View("~/Areas/Admin/Views/Localization/AddLanguage.cshtml");
        }
    }
}
