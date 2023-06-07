using Microsoft.EntityFrameworkCore;
using University.DataAccess;
using University.Models.DataModels;

namespace University.Services
{
    public class CourseService : ICourseService
    {
        private readonly UniversityDBContext _context;

        public CourseService (UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCourseInXLevelWithActiveStudents(Level level)
        {
            var coursesWithActiveStudentsInThisLevel = await _context.Courses
                .Where(courses => courses.Level == level && courses.Students.Count != 0)
                .ToArrayAsync();

            if (!coursesWithActiveStudentsInThisLevel.Any())
            {
                throw new Exception("There is no course in " + level + " with active students");
            }

            return coursesWithActiveStudentsInThisLevel;
        }

        public async Task<IEnumerable<Course>> GetCoursesinXLevel(Level level)
        {
            var coursesInThisLevel = await _context.Courses
                .Where(courses => courses.Level == level)
                .ToListAsync();

            if (!coursesInThisLevel.Any())
            {
                throw new Exception("Can't find courses in level: " + level);
            }

            return coursesInThisLevel;
        }

        public async Task<IEnumerable<Course>> GetCoursesOfStudent(int studentId)
        {
            var coursesOfThisStudent = await _context.Courses
                .Where(course => course.Students
                .Any(student => student.Id == studentId))
                .ToListAsync();

            if (!coursesOfThisStudent.Any())
            {
                throw new Exception("Can't find course for student with ID: " + studentId);
            }

            return coursesOfThisStudent;
        }

        public async Task<IEnumerable<Course>> GetCoursesWithoutChapter()
        {
            var coursesWithoutChapters = await _context.Courses
                .Where(course => course.Chapter == null)
                .ToListAsync();

            if (!coursesWithoutChapters.Any()) 
            {
                throw new Exception("Can't find courses without chapter");
            }
            return coursesWithoutChapters;
        }

        public async Task<IEnumerable<Course>> GetCourseWithoutStudents()
        {
            var coursesWithoutStudents = await _context.Courses
                .Where(courses => courses.Students.Count == 0)
                .ToListAsync();

            if (coursesWithoutStudents.Count == 0)
            {
                throw new Exception("Can't find courses without students enrolled.");
            }

            return coursesWithoutStudents;
        }

        public async Task<IEnumerable<Course>> GetCourseWithStudents()
        {
            var coursesWithStudents = await _context.Courses
                .Where(course => course.Students.Count != 0)
                .ToListAsync();

            if (!coursesWithStudents.Any())
            {
                throw new Exception("Can't find courses with students");
            }

            return coursesWithStudents;
        }




    }
}
