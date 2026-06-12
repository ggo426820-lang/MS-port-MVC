using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Areas.Admin.Components.ViewComponents;

public class SystemStatusViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var status = new
        {
            DbConnected  = true,
            Uptime       = FormatUptime(DateTime.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime()),
            MemoryMb     = (int)(GC.GetTotalMemory(false) / 1048576),
            Environment  = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
        };
        ViewBag.Status = status;
        return View("~/Areas/Admin/Views/Shared/Components/SystemStatus/Default.cshtml");
    }

    private static string FormatUptime(TimeSpan ts)
    {
        if (ts.TotalDays >= 1)  return $"{(int)ts.TotalDays}d {ts.Hours}h";
        if (ts.TotalHours >= 1) return $"{ts.Hours}h {ts.Minutes}m";
        return $"{ts.Minutes}m";
    }
}
