using Microsoft.AspNetCore.Mvc;
using Cookie.Models;

namespace Cookie.Controllers
{
    public class LoginController : Controller
    {
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
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
                Response.Cookies.Append("login", login.UserName, option); // создание куки
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }
    }
}
