using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the First Name is 20 characters.")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Last Name is 50 characters.")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Age is a required field.")]
        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string Position { get; set; }
        
        public string Address { get; set; }

        public string City { get; set; }
    }
}