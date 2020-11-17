using System;

namespace Entities.DTOs
{
    public class CompanyDto : DomainEntity<Guid>
    {
        public string Name { get; set; }
        
        public string FullAddress { get; set; }

        public string Country { get; set; }
    }
}