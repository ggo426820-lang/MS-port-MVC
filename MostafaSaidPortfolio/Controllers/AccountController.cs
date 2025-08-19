using Microsoft.AspNetCore.Mvc;

namespace MostafaSaidPortfolio.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();
        public IActionResult Register() => View();
        public IActionResult Profile() => View();
        public IActionResult Settings() => View();
        public IActionResult ForgotPassword() => View();
        public IActionResult ResetPassword() => View();
        public IActionResult TwoFactor() => View();
    }
}
