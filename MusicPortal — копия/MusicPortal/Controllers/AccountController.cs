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
        private readonly MusicPortalContext _context;
        IEncryption _sender;
        private readonly IRepository _repo;
        public AccountController(MusicPortalContext context, IEncryption sender, IRepository rep)
        {
            _context = context;
            _sender= sender;
            _repo = rep;
        }

        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel reg)
        {
            User user = new User();
            user.Login = reg.Login!;
            user.Status = (reg.Login == "admin") ? 2 : 0;

            if (ModelState.IsValid)
            {
                string salt = _sender.Encryptyion(reg);
                user.Password = _sender.Encryptyion(reg, salt);
                _context.Users.Add(user);
                _context.SaveChanges();
                _sender.SaveSaltToDatabase(reg, salt);
                _context.SaveChangesAsync();
                HttpContext.Session.SetString("login", user.Login);
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
                var curUser = await _context.Users.Where(x => x.Login == logon.Login).SingleAsync();
                if (curUser == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                if (curUser.Status == 0)
                {
                    ModelState.AddModelError("", "User not approved!");
                    return View(logon);
                }

                string hash = _sender.Decryption(curUser, logon);

                if (curUser.Password != hash)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("login", curUser.Login);
                HttpContext.Session.SetInt32("Id", curUser.Id);

                if (curUser.Status == 2)
                    return RedirectToAction("Index", "Admin");
                else 
                    return RedirectToAction("Index", "User");
               
            }
            return View(logon);
        }

    }
}
