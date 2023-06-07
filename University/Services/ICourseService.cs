using University.Models.DataModels;

namespace University.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCourseWithoutStudents();
        Task<IEnumerable<Course>> GetCourseWithStudents();
        Task<IEnumerable<Course>> GetCourseInXLevelWithActiveStudents(Level level);
        Task<IEnumerable<Course>> GetCoursesOfStudent(int  studentId);
        Task<IEnumerable<Course>> GetCoursesinXLevel(Level level);
        Task<IEnumerable<Course>> GetCoursesWithoutChapter();
    }
}
