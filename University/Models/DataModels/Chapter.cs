using System.ComponentModel.DataAnnotations;

namespace University.Models.DataModels
{
    public class Chapter : BaseEntity //TEMARIO OCONTENIDO DE UNA MATERIA O CURSO
    {
        [Required]
        public string List = string.Empty;

        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;

    }
}
