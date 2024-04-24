using Microsoft.EntityFrameworkCore;

namespace AjaxMvcApplication.Models
{
    // Чтобы подключиться к базе данных через Entity Framework, необходим контекст данных. 
    // Контекст данных представляет собой класс, производный от класса DbContext.
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public StudentContext(DbContextOptions<StudentContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                Students.Add(new Student { Name = "Максим", Surname = "Иваненко", Age = 20, GPA = 10.5 });
                Students.Add(new Student { Name = "Ольга", Surname = "Алексеенко", Age = 23, GPA = 11.5 });
                Students.Add(new Student { Name = "Сергей", Surname = "Петренко", Age = 25, GPA = 12 });
                SaveChanges();
            }
        }
    }
}