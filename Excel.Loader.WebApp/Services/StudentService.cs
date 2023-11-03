using Excel.Loader.WebApp.Context;
using Excel.Loader.WebApp.Models;

namespace Excel.Loader.WebApp.Services
{
    public class StudentService : IStudentService
    {
        DatabaseContext _dbContext;

        public StudentService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Student> GetStudents()
        {
            return _dbContext.Students.ToList();
        }

        public List<Student> SaveStudents(List<Student> students)
        {
            _dbContext.Students.AddRange(students);
            
            return students;
        }
    }
}
