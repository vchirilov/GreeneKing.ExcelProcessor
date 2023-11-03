using Excel.Loader.WebApp.Models;

namespace Excel.Loader.WebApp.Services
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        List<Student> SaveStudents(List<Student> students);
    }
}
