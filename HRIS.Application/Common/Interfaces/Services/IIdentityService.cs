using HRIS.Application.Common.Models;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);
    }
}
