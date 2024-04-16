using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;
using MusicPortal.Services;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository _repo;

        public AccountController(IRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                var user = await _repo.GetUserByLoginAsync(reg.Login);
                if (user != null)
                {
                    ModelState.AddModelError("", "That login already exists!");
                    return View(reg);
                }
                await _repo.AddUserAsync(reg);
                HttpContext.Session.SetString("login", reg.Login);
                return RedirectToAction("Login");
            }

            return View(reg);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var user = await _repo.GetUserByLoginAsync(logon.Login);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                if (user.Status == 0)
                {
                    ModelState.AddModelError("", "User not approved!");
                    return View(logon);
                }

                if (!await _repo.VerifyPasswordAsync(user, logon.Password))
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("login", user.Login);
                HttpContext.Session.SetInt32("Id", user.Id);

                if (user.Status == 2)
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "User");

            }
            return View(logon);
        }

    }
}
