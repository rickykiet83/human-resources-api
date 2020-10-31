using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class CompanyManipulationDto
    {
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for the Address is 250 characters.")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Country is a required field.")]
        public string Country { get; set; }
    }
}