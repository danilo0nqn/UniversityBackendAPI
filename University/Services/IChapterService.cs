using University.Models.DataModels;

namespace University.Services
{
    public interface IChapterService
    {
        Task<Chapter> GetChapterOfCourse(int courseId);
    }
}
