using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMVC.Models;
using StudentsMVC.Repository;

namespace StudentsMVC.Controllers
{
    public class StudentsController : Controller
    {
        IRepository repo;

        public StudentsController(IRepository r)
        {
            repo = r;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var model = await repo.GetStudentList();
            return View(model);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await repo.GetStudentList() == null)
            {
                return NotFound();
            }
            var student = await repo.GetStudent((int)id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Age,GPA")] Student student)
        {
            if (ModelState.IsValid)
            {
                await repo.Create(student);
                await repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await repo.GetStudentList() == null)
            {
                return NotFound();
            }
            var student = await repo.GetStudent((int)id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Age,GPA")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.Update(student);
                    await repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await repo.GetStudentList() == null)
            {
                return NotFound();
            }

            var student = await repo.GetStudent((int)id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await repo.GetStudentList() == null)
            {
                return Problem("Entity set 'StudentContext.Students'  is null.");
            }
            var student = await repo.GetStudent(id);
            if (student != null)
            {
                await repo.Delete(id);
            }

            await repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StudentExists(int id)
        {
            List<Student> list = await repo.GetStudentList();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}