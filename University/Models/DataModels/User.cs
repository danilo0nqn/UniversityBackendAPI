using System.ComponentModel.DataAnnotations;

namespace University.Models.DataModels
{
   


    public class User : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(50)]
        [RegularExpression("^(Admin|User 1)$", ErrorMessage = "UserType must be either 'Admin' or 'User 1'.")]
        public string UserType { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public Student? Student { get; set; }
    }
}
