using System;
using Entities;
using Entities.Models;
using HumanResourceAPI.Infrastructure;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company, Guid>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }
    }
}