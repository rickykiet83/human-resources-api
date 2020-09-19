using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Employee : DomainEntity<Guid>
    {
        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the First Name is 20 characters.")]
        public string FistName { get; set; }
        
        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Last Name is 50 characters.")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string Position { get; set; }
        
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        
        public Company Company { get; set; }
    }
}