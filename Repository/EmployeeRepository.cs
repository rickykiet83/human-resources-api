using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using HumanResourceAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindAll(trackChanges, e => e.CompanyId.Equals(companyId))
                .OrderBy(e => e.FirstName)
                .ToListAsync();
        
            return PagedList<Employee>
                .ToPagedList(employees, employeeParameters.PageNumber,
                    employeeParameters.PageSize);
        }

        public async Task<IEnumerable<Employee>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindAll(trackChanges, e => ids.Contains(e.Id)).ToListAsync();
    }
}