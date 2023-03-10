using System;

namespace Entities.DTOs
{
    public class EmployeeDto : DomainEntity<Guid>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int Age { get; set; }
        
        public string Position { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
    }
}