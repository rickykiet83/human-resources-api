using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "FPT Software",
                    Address = "Duy Tan Street, Dich Vong Hau Ward, Cau Giay District, Hanoi City, Vietnam",
                    Country = "Vietnam"
                },
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Apple Ltd",
                    Address = "312 Forest Avenue, California",
                    Country = "USA"
                }
            );

        }
    }
}