using System.ComponentModel.DataAnnotations;

namespace University.Models.DataModels
{
    public enum Level
    {
        [Display(Name = "Basic")]
        Basic,
        [Display(Name = "Medium")]
        Medium,
        [Display(Name = "Advanced")]
        Advanced,
        [Display(Name = "Expert")]
        Expert,
    }
    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Objetives { get; set; } = string.Empty;

        [Required]
        public string Requirement { get; set; } = string.Empty;

        [Required]
        public Level Level { get; set; } = Level.Basic;

        public ICollection<Category> Categories { get; set; } = new List<Category>(); // A course can belong (have) a list of categories.

        public ICollection<Student> Students { get; set; } = new List<Student>(); // A course can have many students

        public Chapter? Chapter { get; set; }
    }
}
