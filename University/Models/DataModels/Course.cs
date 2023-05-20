using System.ComponentModel.DataAnnotations;

namespace University.Models.DataModels
{
    public enum Level
    {
        Basic = 0,
        Medium = 1,
        Advanced = 2,
        Expert = 3,
    }
    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Objetives { get; set; } = string.Empty;

        [Required]
        public string Requirement { get; set; } = string.Empty;

        [Required]
        public Level Level { get; set; } = Level.Basic;

    }
}
