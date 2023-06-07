using Microsoft.EntityFrameworkCore;
using University.DataAccess;
using University.Models.DataModels;

namespace University.Services
{
    public class ChapterService : IChapterService
    {
        readonly UniversityDBContext _context;

        public ChapterService(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<Chapter> GetChapterOfCourse(int courseId)
        {
            var chapterOfThisCourse = await _context.Chapter
                .Where(chapter => chapter.Course.Id == courseId)
                .FirstOrDefaultAsync();

            if (chapterOfThisCourse == null)
            {
                throw new Exception("Can't find the chapter for course with ID: "+ courseId);
            }
            return chapterOfThisCourse;
        }
    }
}
