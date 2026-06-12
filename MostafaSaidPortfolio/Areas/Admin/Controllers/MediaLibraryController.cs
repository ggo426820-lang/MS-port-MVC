using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MediaLibraryController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public MediaLibraryController(IWebHostEnvironment env) { _env = env; }

        public IActionResult Index()
        {
            ViewData["Title"]      = "Media Library";
            ViewData["Breadcrumb"] = "Admin / Media Library";
            var wwwroot = _env.WebRootPath;
            var imgDir  = Path.Combine(wwwroot, "images");
            var files   = Directory.Exists(imgDir)
                ? Directory.GetFiles(imgDir, "*", SearchOption.AllDirectories)
                    .Select(f => new
                    {
                        Name     = Path.GetFileName(f),
                        Url      = "/images/" + Path.GetRelativePath(imgDir, f).Replace("\\", "/"),
                        Size     = new FileInfo(f).Length,
                        Modified = new FileInfo(f).LastWriteTimeUtc,
                        Ext      = Path.GetExtension(f).ToLower()
                    })
                    .OrderByDescending(f => f.Modified)
                    .ToList<dynamic>()
                : new List<dynamic>();
            ViewData["Files"] = files;
            return View("~/Areas/Admin/Views/MediaLibrary/Index.cshtml");
        }

        public IActionResult Gallery()
        {
            ViewData["Title"] = "Gallery";
            return View("~/Areas/Admin/Views/MediaLibrary/Gallery.cshtml");
        }

        public IActionResult Upload()
        {
            ViewData["Title"] = "Upload Media";
            return View("~/Areas/Admin/Views/MediaLibrary/Upload.cshtml");
        }

        public IActionResult Storage()
        {
            ViewData["Title"] = "Storage Info";
            var wwwroot = _env.WebRootPath;
            var imgDir  = Path.Combine(wwwroot, "images");
            long totalBytes = 0;
            int  totalFiles = 0;
            if (Directory.Exists(imgDir))
            {
                var fi = Directory.GetFiles(imgDir, "*", SearchOption.AllDirectories);
                totalFiles = fi.Length;
                totalBytes = fi.Sum(f => new FileInfo(f).Length);
            }
            ViewData["TotalFiles"] = totalFiles;
            ViewData["TotalMB"]    = (totalBytes / 1024.0 / 1024.0).ToString("F2");
            return View("~/Areas/Admin/Views/MediaLibrary/Storage.cshtml");
        }

        public IActionResult Details(string name)
        {
            ViewData["Title"] = "File Details";
            ViewData["FileName"] = name;
            return View("~/Areas/Admin/Views/MediaLibrary/Details.cshtml");
        }
    }
}
