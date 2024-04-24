using Microsoft.EntityFrameworkCore;
using StudentsMVC.Models;

namespace StudentsMVC.Repository
{
    public class StudentRepository : IRepository
    {
        private readonly StudentContext _context;

        public StudentRepository(StudentContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetStudentList()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student> GetStudent(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task Create(Student st)
        {
            await _context.Students.AddAsync(st);
        }

        public void Update(Student st)
        {
            _context.Update(st);
        }

        public async Task Delete(int id)
        {
            Student? st = await _context.Students.FindAsync(id);
            if (st != null)
                _context.Students.Remove(st);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }       
    }
}