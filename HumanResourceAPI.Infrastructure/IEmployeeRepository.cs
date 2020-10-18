using System;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace HumanResourceAPI.Infrastructure
{
    public interface IEmployeeRepository : IRepositoryBase<Employee, Guid>
    {
        Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters,
            bool trackChanges);
    }
}