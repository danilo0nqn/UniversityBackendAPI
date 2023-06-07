using University.Models.DataModels;

namespace University.Services
{
    public interface IStudentService
    {
        //Active students

        Task<IEnumerable<Student>> GetActiveStudents();
        Task<IEnumerable<Student>> GetInactiveStudents();
        Task<IEnumerable<Student>> GetStudentsOlderThan18();
        Task<IEnumerable<Student>> GetStudentsInCourse(int courseID);
    }
}
