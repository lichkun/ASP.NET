using Microsoft.AspNetCore.Mvc;
using Session.Models;

namespace Session.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Login login)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("login", login.UserName); // создание сессионной переменной
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }
    }
}
