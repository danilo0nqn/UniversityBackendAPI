using Microsoft.EntityFrameworkCore;
using University.DataAccess;
using University.Models.DataModels;

namespace University.Services
{
    public class StudentService : IStudentService
    {
        private readonly UniversityDBContext _context;
        public StudentService(UniversityDBContext context) 
        {
            _context = context;
        }
        //TODO: resolve methods
        public async Task<IEnumerable<Student>> GetActiveStudents()
        {
            var activeStudents = await _context.Students
                .Where(student => student.Courses
                .Any())
                .ToListAsync();

            if (!activeStudents.Any())
            {
                throw new Exception("Can't find active students");
            }

            return activeStudents;
        }

        public async Task<IEnumerable<Student>> GetInactiveStudents()
        {
            var inactiveStudents = await _context.Students
                .Where(student => student.Courses
                .Any())
                .ToListAsync();

            if (!inactiveStudents.Any())
            {
                throw new Exception("Can't find students who aren't enrolled in any course.");
            }
            return inactiveStudents;
        }

        public async Task<IEnumerable<Student>> GetStudentsInCourse(int courseID)
        {
            var studentsInThisCourse = await _context.Students
                .Where(student => student.Courses
                .Any(course => course.Id == courseID))
                .ToListAsync();

            if (!studentsInThisCourse.Any())
            {
                throw new Exception("Can't find students enrolled in course with ID: " + courseID);
            }

            return studentsInThisCourse;
        }

        public async Task<IEnumerable<Student>> GetStudentsOlderThan18()
        {
            int currentYear = DateTime.Now.Year;
            var studentsOlderThan18 = await _context.Students
                .Where(student => currentYear - student.DateOfBirth.Year > 18)
                .ToListAsync();

            if (!studentsOlderThan18.Any())
            {
                throw new Exception("Can't find adults.");
            }

            return studentsOlderThan18;
        }
    }
}
