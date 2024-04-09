using Microsoft.AspNetCore.Mvc;

namespace StudentsMVC.Controllers
{
    public class HomeController : Controller
    {
        // ContentResult: пишет указанный контент напрямую в ответ в виде строки
        // Если в качестве возвращаемого результата тип string, то фреймворк 
        // автоматически создаст объект ContentResult для возвращаемой строки.
        public string Square(int a, int h)
        {
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a +
                    " и высотой " + h + " равна " + s + "</h2>";
        }
        
        public IActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет, мир!</h2>");
        }

        public IActionResult GetFile()
        {
            // Путь к файлу
            string file_path = "~/Image/IMG_20170504_170840.jpg";
            // Тип файла - content-type
            string file_type = "image/jpeg";
            // Имя файла - необязательно
            string file_name = "Капри.jpg";
            return File(file_path, file_type, file_name);
        }

        public ViewResult SomeMethod()
        {
            ViewBag.Name = "MS SQL Server";
            ViewData["Head"] = "Entity Framework Core";
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Index()
        {
            ViewBag.Name = "ASP.NET Core MVC";
            ViewData["Head"] = "ASP.NET Core Razor Pages";
            return View();
        }

        public RedirectResult RedirectMethod()
        {
            return Redirect("/Home/Index");
        }
    }
}

