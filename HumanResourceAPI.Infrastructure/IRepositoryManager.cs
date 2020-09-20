using System.Threading.Tasks;

namespace HumanResourceAPI.Infrastructure
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
    }
}