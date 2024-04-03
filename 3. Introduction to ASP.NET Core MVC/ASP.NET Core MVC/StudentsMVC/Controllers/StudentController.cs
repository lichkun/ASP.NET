using Microsoft.AspNetCore.Mvc;

namespace StudentsMVC
{
    public class StudentController : Controller
    {
        StudentContext db;
        public StudentController(StudentContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Student> students = await Task.Run(() => db.Students);
            ViewBag.Students = students;
            return View();
        }
    }
}
