using StudentsMVC.Models;

namespace StudentsMVC.Repository
{
    public interface IRepository
    {
        Task<List<Student>> GetStudentList();
        Task<Student> GetStudent(int id);
        Task Create(Student item);
        void Update(Student item);
        Task Delete(int id);
        Task Save();
    }
}
