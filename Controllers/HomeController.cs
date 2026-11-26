using Microsoft.AspNetCore.Mvc;

namespace DriveHub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return Redirect("/Privacy");
        }

        public IActionResult About()
        {
            ViewData["Title"] = "About Drive Hub";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Redirect("/Error");
        }
    }
}