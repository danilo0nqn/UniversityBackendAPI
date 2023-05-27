using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using University.DataAccess;

namespace University.Models.DataModels
{
    public class Services
    {
        private readonly UniversityDBContext _context;
        public Services(UniversityDBContext context)
        {
            _context = context;
        }

        public bool UserExistsWithThisEmail(string email)
        {
            return (_context.Users?.Any(e => e.Email == email)).GetValueOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            var userWithDeterminedEmail = _context.Users?.FirstOrDefault(e => e.Email == email);

            if (userWithDeterminedEmail == null)
            {
                throw new Exception("Cant find user with " +  email + " email");
            }

            return userWithDeterminedEmail;
        }

        public List<Student> GetStudentOlderThan18()
        {
            int currentYear = DateTime.Now.Year;
            var studentsOlderThan18 = _context.Students
                .Where(student => currentYear - student.DateOfBirth.Year > 18)
                .ToList();

            if (studentsOlderThan18.Count == 0)
            {
                throw new Exception("Can't find adults.");
            }

            return studentsOlderThan18;
        }

        public List<Student> GetActiveStudent()
        {
            var activeStudents = _context.Students
                .Where(student => student.Courses
                .Any())
                .ToList();

            if (activeStudents.Count == 0)
            {
                throw new Exception("Can't find active students");
            }

            return activeStudents;
        }

        public List<Course> GetCourseInXLevelWithActiveStudents(Level level)
        {
            var coursesInDeterminedLevelWithActiveStudents = _context.Courses
                .Where(courses => courses.Level == level && courses.Students
                .Any())
                .ToList();

            if (coursesInDeterminedLevelWithActiveStudents.Count == 0)
            {
                throw new Exception("Can't find active students enrolled in " + level + " level courses.");
            }

            return coursesInDeterminedLevelWithActiveStudents;
        }

        public List<Course> GetCourseWithoutStudents()
        {
            var coursesWithoutStudents = _context.Courses
                .Where(courses => courses.Students.Count == 0)
                .ToList();

            if (coursesWithoutStudents.Count == 0)
            {
                throw new Exception("Can't find courses without students enrolled.");
            }

            return coursesWithoutStudents;
        }

    }
}
