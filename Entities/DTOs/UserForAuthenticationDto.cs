using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}