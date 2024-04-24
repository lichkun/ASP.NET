using AjaxMvcApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AjaxMvcApplication.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            if (_context.Students == null)
                return Problem("Список студентов пустой!");
            List<Student> list = await _context.Students.ToListAsync();
            string response = JsonConvert.SerializeObject(list);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(student);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> InsertStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                string response = "Студент успешно добавлен!";
                return Json(response);
            }
            return Problem("Проблемы при добавлении студента!");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
                string response = "Студент успешно изменен!";
                return Json(response);
            }
            return Problem("Проблемы при редактировании студента!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Список студентов пустой!");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            else 
            {
                return Problem("Нет такого студента!");
            }
            await _context.SaveChangesAsync();
            string response = "Студент успешно удален!";
            return Json(response);
        }
    }
}