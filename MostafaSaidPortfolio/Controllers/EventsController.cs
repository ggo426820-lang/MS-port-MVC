using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService) => _eventsService = eventsService;

        public async Task<IActionResult> Index()
        {
            var all = await _eventsService.GetAllAsync();
            var now = DateTime.UtcNow;
            ViewBag.Upcoming = all.Where(e => e.EventDate >= now).OrderBy(e => e.EventDate).ToList();
            ViewBag.Past     = all.Where(e => e.EventDate  < now).OrderByDescending(e => e.EventDate).ToList();
            return View();
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var ev = await _eventsService.GetByIdAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        public async Task<IActionResult> Register(Guid id)
        {
            var ev = await _eventsService.GetByIdAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }
    }
}
