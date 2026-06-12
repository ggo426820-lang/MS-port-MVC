using Microsoft.AspNetCore.Mvc;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialsController(ITestimonialService testimonialService) =>
            _testimonialService = testimonialService;

        public async Task<IActionResult> Index()
        {
            var testimonials = await _testimonialService.GetApprovedAsync();
            return View(testimonials);
        }

        public IActionResult Submit() => View();
    }
}
