using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using System.Security.Cryptography;
using System.Text;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.Attributes;

namespace MusicPortal.Controllers
{
    [Culture]
    public class AccountController : Controller
    {
        IAccount accountService;
        public AccountController(IAccount accS)
        {
            accountService= accS;
        } 

        public ActionResult Register()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                var user = await accountService.GetUserByLoginAsync(reg.Login);
                if (user != null)
                {
                    ModelState.AddModelError("", "That login already exists!");
                    return View(reg);
                }
                UserDTO userDTO = new UserDTO() 
                { 
                    Login = reg.Login,
                    Password = reg.Password   
                };
                await accountService.AddUserAsync(userDTO);
                HttpContext.Session.SetString("login", reg.Login);
                return RedirectToAction("Login");
            }

            return View(reg);
        }

        public ActionResult Login()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var user = await accountService.GetUserByLoginAsync(logon.Login);
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

                if (!await accountService.VerifyPasswordAsync(user, logon.Password))
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
