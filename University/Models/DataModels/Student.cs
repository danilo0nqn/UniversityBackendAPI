using System.ComponentModel.DataAnnotations;

namespace University.Models.DataModels
{
    public class Student : BaseEntity
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>(); // a student can be in many courses


    }
}
