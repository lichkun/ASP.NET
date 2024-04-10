using AccountMVC.Repository;
using Guests.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoringPassword.Models;
using System.Security.Cryptography;
using System.Text;

namespace Guests.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository _repo;
        public AccountController(IRepository context)
        {
            _repo = context;
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
                var curUser = await _repo.GetUser(logon);
                if (curUser == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }

                string hash = await _repo.Decryption(curUser, logon);

                if (curUser.Password != hash)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("login", curUser.Login);

                return RedirectToAction("Index");
            }
            return View(logon);
        }

        public IActionResult Register()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                _repo.Register(reg);
                _repo.Save();
                return RedirectToAction("Login");
            }

            return View(reg);
        }
        public async Task<IActionResult> Index()
        {
           var mesVM = await _repo.GetMessagesList();

            return View(mesVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(MessagesViewModel mesVM)
        {
            var userlist = await _repo.GetUsersList();
            var curentUser = userlist.FirstOrDefault(curentUser => curentUser.Login == HttpContext.Session.GetString("login"));
            if (curentUser != null)
            {
                Messages mes = new Messages
                {
                    User = curentUser, 
                    MessageText = mesVM.NewMessage.MessageText,
                    Date = DateOnly.FromDateTime(DateTime.Now)
                };
                await _repo.AddMessage(mes);
                await _repo.Save();
            }
            return RedirectToAction("Index");
        }

    }
}
