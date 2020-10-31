using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace HumanResourceAPI.Infrastructure
{
    public interface ICompanyRepository : IRepositoryBase<Company, Guid>
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        
        Task<PagedList<Company>> GetCompaniesAsync(CompanyParameters companyParameters, bool trackChanges);
    }
}