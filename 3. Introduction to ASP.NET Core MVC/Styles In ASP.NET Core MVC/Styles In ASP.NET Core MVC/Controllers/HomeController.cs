using Microsoft.AspNetCore.Mvc;

namespace Images_In_ASP.NET_Core_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}