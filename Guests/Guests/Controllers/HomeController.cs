using Guests.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Guests.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MessengerContext _context;

        public HomeController(ILogger<HomeController> logger, MessengerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            Messages messages = new Messages();
            return View(messages);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
       
        
    }
}
