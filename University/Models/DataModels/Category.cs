using System.ComponentModel.DataAnnotations;

namespace University.Models.DataModels
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Course> Course { get; set; } = new List<Course>(); // A category can have a list of courses
    }
}
