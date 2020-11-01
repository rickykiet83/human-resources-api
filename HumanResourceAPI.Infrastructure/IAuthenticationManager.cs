using System.Threading.Tasks;
using Entities.DTOs;

namespace HumanResourceAPI.Infrastructure
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}